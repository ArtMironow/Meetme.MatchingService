using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;

namespace Meetme.MatchingService.Application.Notifications.Queries.GetByUserId;

public class GetNotificationsByUserIdQueryHandler : IRequestHandler<GetNotificationsByUserIdQuery, GetNotificationsByUserIdResult>
{
    private readonly IMongoRepository _mongoRepository;

	public GetNotificationsByUserIdQueryHandler(IMongoRepository mongoRepository)
	{
		_mongoRepository = mongoRepository;
	}

	public async Task<GetNotificationsByUserIdResult> Handle(GetNotificationsByUserIdQuery request, CancellationToken cancellationToken)
	{
		var notificationEvents = await _mongoRepository.FindEventsByUserIdAsync(request.Id, cancellationToken);

		return new GetNotificationsByUserIdResult(notificationEvents);
	}
}
