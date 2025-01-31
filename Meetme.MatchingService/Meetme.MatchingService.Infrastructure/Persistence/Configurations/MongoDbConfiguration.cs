using Meetme.MatchingService.Domain.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Meetme.MatchingService.Infrastructure.Persistence.Configurations;

public static class MongoDbConfiguration
{
	public static void Configure()
	{
		BsonClassMap.RegisterClassMap<EventDetails>(cm =>
		{
			cm.AutoMap();
			cm.MapMember(c => c.MatchedProfileId)
				.SetSerializer(new NullableSerializer<Guid>(new GuidSerializer(GuidRepresentation.Standard)));
		});
	}
}
