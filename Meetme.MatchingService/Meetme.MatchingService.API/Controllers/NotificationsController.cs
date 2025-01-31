using MapsterMapper;
using Meetme.MatchingService.API.Common.Routes;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Contracts.Notifications.GetByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.MatchingService.API.Controllers;

[ApiController]
[Authorize]
[Route(EndpointRoutes.MatchNotifications)]
public class NotificationsController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly INotificationService _notificationService;

	public NotificationsController(IMapper mapper, INotificationService notificationService)
	{
		_mapper = mapper;
		_notificationService = notificationService;
	}

	[HttpGet]
	public async Task<IEnumerable<GetNotificationsByUserIdResponse>> GetNotificationsByUserIdAsync(CancellationToken cancellationToken)
	{
		var notifications = await _notificationService.GetNotificationsByUserIdAsync(User, cancellationToken);

		var notificationsResponse = _mapper.Map<IEnumerable<GetNotificationsByUserIdResponse>>(notifications);

		return notificationsResponse;
	}
}
