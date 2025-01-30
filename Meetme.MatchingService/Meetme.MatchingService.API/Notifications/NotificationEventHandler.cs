using MediatR;
using Meetme.MatchingService.API.Common.Notifications;
using Meetme.MatchingService.Domain.Events;
using Microsoft.AspNetCore.SignalR;

namespace Meetme.MatchingService.API.Notifications;

public class NotificationEventHandler : INotificationHandler<NotificationEvent>
{
	private readonly IHubContext<NotificationsHub> _hubContext;

	public NotificationEventHandler(IHubContext<NotificationsHub> hubContext)
	{
		_hubContext = hubContext;
	}

	public Task Handle(NotificationEvent notificationEvent, CancellationToken cancellationToken)
	{
		return _hubContext.Clients.User(notificationEvent.UserId.ToString()).SendAsync(NotificationsKeys.MethodName, notificationEvent, cancellationToken);
	}
}
