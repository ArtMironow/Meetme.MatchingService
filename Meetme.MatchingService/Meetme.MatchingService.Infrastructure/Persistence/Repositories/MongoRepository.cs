using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.Events;
using Meetme.MatchingService.Infrastructure.Common;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Meetme.MatchingService.Infrastructure.Persistence.Repositories;

public class MongoRepository : IMongoRepository
{
    private readonly IMongoCollection<NotificationEvent> _eventCollection;

	public MongoRepository(IOptions<ConnectionStrings> connectionStringsAccessor)
	{
		var mongoClient = new MongoClient(connectionStringsAccessor.Value.MongoDbConnection);
		var mongoDbName = mongoClient.GetDatabase(ConfigurationKeys.MongoDbName);

		_eventCollection = mongoDbName.GetCollection<NotificationEvent>(ConfigurationKeys.MongoDbCollectionName);
	}

	public Task<List<NotificationEvent>> FindEventsByUserIdAsync(string id, CancellationToken cancellationToken)
	{
		return _eventCollection.Find(x => x.UserId == id).ToListAsync(cancellationToken);
	}

	public Task SaveAsync(NotificationEvent @event, CancellationToken cancellationToken)
	{
		return _eventCollection.InsertOneAsync(@event, new InsertOneOptions(), cancellationToken);
	}
}
