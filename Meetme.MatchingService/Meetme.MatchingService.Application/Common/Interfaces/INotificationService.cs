using Meetme.MatchingService.Domain.DataTransferObjects;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface INotificationService
{
	Task SendProfilesMatchedNotificationAsync(ProfileDto profile, Guid matchedProfileId, CancellationToken cancellationToken);
}
