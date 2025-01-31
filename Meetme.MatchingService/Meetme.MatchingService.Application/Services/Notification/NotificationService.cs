using MapsterMapper;
using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;
using Meetme.MatchingService.Domain.DataTransferObjects;
using Meetme.MatchingService.Domain.Events;
using Meetme.MatchingService.Domain.Events.Enums;
using MongoDB.Bson;
using System.Security.Claims;

namespace Meetme.MatchingService.Application.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly IMediator _mediator;
	private readonly IMapper _mapper;

	public NotificationService(IMediator mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	public Task SendProfilesMatchedNotificationAsync(ProfileDto profile, Guid matchedProfileId, CancellationToken cancellationToken)
    {
		var notificationEvent = new NotificationEvent
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserId = profile.IdentityId,
			ProfileId = profile.Id,
			EventDetails = new()
			{
				MatchedProfileId = matchedProfileId,
			},
			Type = NotificationType.Match,
			CreatedAt = DateTime.UtcNow,
		};

		return _mediator.Publish(notificationEvent, cancellationToken);
	}

	public async Task<GetNotificationsByUserIdResult> GetNotificationsByUserIdAsync(ClaimsPrincipal user, CancellationToken cancellationToken)
	{
		var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		var getNotificationsQuery = new GetNotificationsByUserIdQuery(userId!);

		var notifications = await _mediator.Send(getNotificationsQuery, cancellationToken);

		return notifications;
	}
}
