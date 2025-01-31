using MediatR;
using Meetme.MatchingService.API.Common.Notifications;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.Events;
using Microsoft.AspNetCore.SignalR;

namespace Meetme.MatchingService.API.Notifications;

public class NotificationEventHandler : INotificationHandler<NotificationEvent>
{
	private readonly IHubContext<NotificationsHub> _hubContext;
	private readonly IMongoRepository _mongoRepository;

	public NotificationEventHandler(IHubContext<NotificationsHub> hubContext, IMongoRepository mongoRepository)
	{
		_hubContext = hubContext;
		_mongoRepository = mongoRepository;
	}

	public async Task Handle(NotificationEvent notificationEvent, CancellationToken cancellationToken)
	{
		await _mongoRepository.SaveAsync(notificationEvent, cancellationToken);

		await _hubContext.Clients.User(notificationEvent.UserId).SendAsync(NotificationsKeys.MethodName, notificationEvent, cancellationToken);
	}
}
