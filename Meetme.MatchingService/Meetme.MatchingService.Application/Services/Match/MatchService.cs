using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Application.Models;
using Meetme.MatchingService.Domain.DataTransferObjects;
using Meetme.MatchingService.Domain.Entities;
using Meetme.MatchingService.Domain.Events;

namespace Meetme.MatchingService.Application.Services.Match;

public class MatchService : IMatchService
{
	private readonly IProfileServiceClient _profileServiceClient;
	private readonly IRepository<MatchEntity> _matchRepository;
	private readonly IRepository<LikeEntity> _likeRepository;
	private readonly IPublisher _mediator;
	private readonly IMongoRepository _mongoRepository;

	public MatchService(IProfileServiceClient profileServiceClient, IRepository<MatchEntity> matchRepository, IRepository<LikeEntity> likeRepository, IMediator mediator, IMongoRepository mongoRepository)
	{
		_profileServiceClient = profileServiceClient;
		_matchRepository = matchRepository;
		_likeRepository = likeRepository;
		_mediator = mediator;
		_mongoRepository = mongoRepository;
	}

	public async Task MatchProfilesAsync(LikeModel likeModel, CancellationToken cancellationToken)
	{
		var (areProfilesMatched, profile, matchedProfile) = await TryMatchProfilesAsync(likeModel, cancellationToken);

		if (areProfilesMatched && profile != null && matchedProfile != null)
		{
			var match = new MatchEntity
			{
				ProfileId = likeModel.ProfileId,
				MatchedProfileId = likeModel.LikedProfileId,
			};

			await _matchRepository.AddAsync(match, cancellationToken);

			var sendNotificationTasks = new[]
			{
				SendNotificationAsync(profile, matchedProfile.Id, cancellationToken),
				SendNotificationAsync(matchedProfile, profile.Id, cancellationToken)
			};

			await Task.WhenAll(sendNotificationTasks);
		}
	}

	public async Task RemoveMatchAsync(LikeModel likeModel, CancellationToken cancellationToken)
	{
		var match = await _matchRepository.GetFirstOrDefaultAsync(m =>
			(m.ProfileId == likeModel.ProfileId && m.MatchedProfileId == likeModel.LikedProfileId)
			|| (m.MatchedProfileId == likeModel.ProfileId && m.ProfileId == likeModel.LikedProfileId), cancellationToken);

		if (match != null)
		{
			await _matchRepository.RemoveAsync(match, cancellationToken);
		}
	}

	private async Task SendNotificationAsync(ProfileDto profile, Guid matchedProfileId, CancellationToken cancellationToken)
	{
		var notificationEvent = new NotificationEvent
		{
			UserId = profile.IdentityId,
			ProfileId = profile.Id,
			MatchedProfileId = matchedProfileId,
			CreatedAt = DateTime.UtcNow,
		};

		await _mongoRepository.SaveAsync(notificationEvent, cancellationToken);

		await _mediator.Publish(notificationEvent, cancellationToken);
	}

	private async Task<(bool areProfilesMatched, ProfileDto? profile, ProfileDto? matchedProfile)> TryMatchProfilesAsync(LikeModel likeModel, CancellationToken cancellationToken)
	{
		var like = await _likeRepository.GetFirstOrDefaultAsync(l => l.ProfileId == likeModel.LikedProfileId && l.LikedProfileId == likeModel.ProfileId, cancellationToken);

		if (like == null)
		{
			return (false, null, null);
		}

		var getProfileTasks = new[]
		{
			_profileServiceClient.GetProfileAsync(likeModel.ProfileId, cancellationToken),
			_profileServiceClient.GetProfileAsync(likeModel.LikedProfileId, cancellationToken)
		};

		var getProfileTasksResults = await Task.WhenAll(getProfileTasks);

		var profile = getProfileTasksResults[0];
		var matchingProfile = getProfileTasksResults[1];

		var areProfilesMatched = AreProfilesMatched(profile, matchingProfile);

		return (areProfilesMatched, profile, matchingProfile);
	}

	private bool AreProfilesMatched(ProfileDto? profile, ProfileDto? matchingProfile)
	{
		if (profile == null || matchingProfile == null || profile.Preference == null)
		{
			return false;
		}	

		return !(matchingProfile.Age < profile.Preference.MinAge
			|| matchingProfile.Age > profile.Preference.MaxAge
			|| matchingProfile.Gender != profile.Preference.GenderPreference);
	}
}
