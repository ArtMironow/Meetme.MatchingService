namespace Meetme.MatchingService.Contracts.Notifications.GetByUserId;

public record GetNotificationsByUserIdResponse(Guid Id, string UserId, Guid ProfileId, Guid? MatchedProfileId, string Type, DateTime CreatedAt);
