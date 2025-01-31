using Meetme.MatchingService.Domain.Events;

namespace Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;

public record GetNotificationsByUserIdResult(IEnumerable<NotificationEvent> Notifications);
