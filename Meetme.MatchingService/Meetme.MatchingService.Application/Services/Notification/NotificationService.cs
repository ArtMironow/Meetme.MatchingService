using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.DataTransferObjects;
using Meetme.MatchingService.Domain.Events;
using Meetme.MatchingService.Domain.Events.Enums;

namespace Meetme.MatchingService.Application.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly IPublisher _mediator;

	public NotificationService(IPublisher mediator)
	{
		_mediator = mediator;
	}

	public Task SendProfilesMatchedNotificationAsync(ProfileDto profile, Guid matchedProfileId, CancellationToken cancellationToken)
    {
		var notificationEvent = new NotificationEvent
		{
			Id = Guid.NewGuid(),
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
}
