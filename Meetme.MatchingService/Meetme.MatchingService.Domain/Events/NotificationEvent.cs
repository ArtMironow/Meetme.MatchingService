using MediatR;
using Meetme.MatchingService.Domain.Events.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Meetme.MatchingService.Domain.Events;

public class NotificationEvent : INotification
{
	[BsonId]
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public Guid Id { get; set; }
	public required string UserId { get; set; }
	[BsonGuidRepresentation(GuidRepresentation.Standard)]
	public Guid ProfileId { get; set; }
	public EventDetails? EventDetails { get; set; }
	[BsonRepresentation(BsonType.String)]
	public NotificationType Type { get; set; }
	public DateTime CreatedAt { get; set; }
}
