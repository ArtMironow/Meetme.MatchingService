namespace Meetme.MatchingService.Contracts.Notifications.GetByUserId;

public record GetNotificationsByUserIdResponse(string Id, string UserId, Guid ProfileId, Guid? MatchedProfileId, string Type, DateTime CreatedAt);
