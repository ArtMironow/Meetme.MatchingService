using Meetme.MatchingService.Application.Common.Interfaces;
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

	public async Task MatchProfilesAsync(LikeEntity likeEntity, CancellationToken cancellationToken)
	{
		var areProfilesMatched = await TryMatchProfilesAsync(likeEntity, cancellationToken);

		if (areProfilesMatched)
		{
			var match = new MatchEntity
			{
				ProfileId = likeEntity.ProfileId,
				MatchedProfileId = likeEntity.LikedProfileId,
			};

			await _matchRepository.AddAsync(match, cancellationToken);
		}
	}

	public async Task RemoveMatchAsync(LikeEntity likeEntity, CancellationToken cancellationToken)
	{
		var match = await _matchRepository.GetFirstOrDefaultAsync(m =>
			m.ProfileId == likeEntity.ProfileId && m.MatchedProfileId == likeEntity.LikedProfileId, cancellationToken);

		if (match != null)
		{
			await _matchRepository.RemoveAsync(match, cancellationToken);
		}
	}

	private async Task<bool> TryMatchProfilesAsync(LikeEntity likeEntity, CancellationToken cancellationToken)
	{
		var like = await _likeRepository.GetFirstOrDefaultAsync(l => l.ProfileId == likeEntity.LikedProfileId && l.LikedProfileId == likeEntity.ProfileId, cancellationToken);

		if (like == null)
		{
			return false;
		}

		var profile = await _profileServiceClient.GetProfileAsync(likeEntity.ProfileId);
		var matchingProfile = await _profileServiceClient.GetProfileAsync(likeEntity.LikedProfileId);

		if (matchingProfile!.Age < profile!.Preference!.MinAge
			&& matchingProfile.Age > profile.Preference.MaxAge
			&& matchingProfile.Gender == profile.Preference.GenderPreference)
		{
			return false;
		}

		return true;
	}
}
