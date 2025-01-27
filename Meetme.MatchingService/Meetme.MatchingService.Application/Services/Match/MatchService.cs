using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Application.Models;
using Meetme.MatchingService.Domain.DataTransferObjects;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Services.Match;

public class MatchService : IMatchService
{
	private readonly IProfileServiceClient _profileServiceClient;
	private readonly IRepository<MatchEntity> _matchRepository;
	private readonly IRepository<LikeEntity> _likeRepository;

	public MatchService(IProfileServiceClient profileServiceClient, IRepository<MatchEntity> matchRepository, IRepository<LikeEntity> likeRepository)
	{
		_profileServiceClient = profileServiceClient;
		_matchRepository = matchRepository;
		_likeRepository = likeRepository;
	}

	public async Task MatchProfilesAsync(LikeModel likeModel, CancellationToken cancellationToken)
	{
		var areProfilesMatched = await TryMatchProfilesAsync(likeModel, cancellationToken);

		if (areProfilesMatched)
		{
			var match = new MatchEntity
			{
				ProfileId = likeModel.ProfileId,
				MatchedProfileId = likeModel.LikedProfileId,
			};

			await _matchRepository.AddAsync(match, cancellationToken);
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

	private async Task<bool> TryMatchProfilesAsync(LikeModel likeModel, CancellationToken cancellationToken)
	{
		var like = await _likeRepository.GetFirstOrDefaultAsync(l => l.ProfileId == likeModel.LikedProfileId && l.LikedProfileId == likeModel.ProfileId, cancellationToken);

		if (like == null)
		{
			return false;
		}

		var getProfileTasks = new[]
		{
			_profileServiceClient.GetProfileAsync(likeModel.ProfileId, cancellationToken),
			_profileServiceClient.GetProfileAsync(likeModel.LikedProfileId, cancellationToken)
		};

		var getProfileTasksResults = await Task.WhenAll(getProfileTasks);

		var profile = getProfileTasksResults[0];
		var matchingProfile = getProfileTasksResults[1];

		return AreProfilesMatched(profile, matchingProfile);
	}

	private bool AreProfilesMatched(ProfileDto? profile, ProfileDto? matchingProfile)
	{
		if (profile == null || matchingProfile == null || profile.Preference == null)
		{
			return false;
		}	

		if (matchingProfile.Age < profile.Preference.MinAge
			&& matchingProfile.Age > profile.Preference.MaxAge
			&& matchingProfile.Gender == profile.Preference.GenderPreference)
		{
			return false;
		}

		return true;
	}
}
