using Meetme.MatchingService.Application.Models;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface IMatchService
{
	Task MatchProfilesAsync(LikeModel likeModel, CancellationToken cancellationToken);
	Task RemoveMatchAsync(LikeModel likeModel, CancellationToken cancellationToken);
}
