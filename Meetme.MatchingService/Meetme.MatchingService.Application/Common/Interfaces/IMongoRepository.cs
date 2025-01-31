using Meetme.MatchingService.Domain.Events;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface IMongoRepository
{
	Task<List<NotificationEvent>> FindEventsByUserIdAsync(string id, CancellationToken cancellationToken);
	Task SaveAsync(NotificationEvent @event, CancellationToken cancellationToken);
}
