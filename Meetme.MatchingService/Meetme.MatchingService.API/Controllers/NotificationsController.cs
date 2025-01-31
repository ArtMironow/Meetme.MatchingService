using MapsterMapper;
using MediatR;
using Meetme.MatchingService.API.Common.Routes;
using Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;
using Meetme.MatchingService.Contracts.Notifications.GetByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Meetme.MatchingService.API.Controllers;

[ApiController]
[Authorize]
[Route(EndpointRoutes.MatchNotifications)]
public class NotificationsController : ControllerBase
{
    private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public NotificationsController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IEnumerable<GetNotificationsByUserIdResponse>> GetNotificationsByUserIdAsync(CancellationToken cancellationToken)
	{
		var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		var getNotificationsByUserIdQuery = new GetNotificationsByUserIdQuery(userId!);

		var getNotificationsByUserIdResult = await _mediator.Send(getNotificationsByUserIdQuery, cancellationToken);

		var getNotificationsByUserIdResponse = _mapper.Map<IEnumerable<GetNotificationsByUserIdResponse>>(getNotificationsByUserIdResult);

		return getNotificationsByUserIdResponse;
	}
}
