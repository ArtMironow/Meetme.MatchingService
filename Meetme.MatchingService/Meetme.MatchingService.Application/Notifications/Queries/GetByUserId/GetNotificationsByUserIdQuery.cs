using MediatR;

namespace Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;

public record GetNotificationsByUserIdQuery(string Id) : IRequest<GetNotificationsByUserIdResult>;
