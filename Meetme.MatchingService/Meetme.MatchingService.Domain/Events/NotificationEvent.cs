using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Meetme.MatchingService.Domain.Events;

public class NotificationEvent : INotification
{
	[BsonId]
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public Guid UserId { get; set; }
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public Guid ProfileId { get; set; }
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public Guid MatchedProfileId { get; set; }
	public DateTime CreatedAt { get; set; }
}
