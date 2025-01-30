using Meetme.MatchingService.Domain.Events;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface IMongoRepository
{
	Task SaveAsync(NotificationEvent @event, CancellationToken cancellationToken);
}
