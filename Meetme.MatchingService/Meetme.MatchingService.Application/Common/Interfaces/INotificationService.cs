using Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;
using Meetme.MatchingService.Domain.DataTransferObjects;
using System.Security.Claims;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface INotificationService
{
	Task SendProfilesMatchedNotificationAsync(ProfileDto profile, Guid matchedProfileId, CancellationToken cancellationToken);
	Task<GetNotificationsByUserIdResult> GetNotificationsByUserIdAsync(ClaimsPrincipal user, CancellationToken cancellationToken);
}
