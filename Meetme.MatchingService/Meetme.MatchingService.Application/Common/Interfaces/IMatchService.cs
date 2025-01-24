using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface IMatchService
{
	Task MatchProfilesAsync(LikeEntity likeEntity, CancellationToken cancellationToken);
	Task RemoveMatchAsync(LikeEntity likeEntity, CancellationToken cancellationToken);
}
