namespace Meetme.MatchingService.Domain.Entities;

public class LikeEntity
{
	public Guid Id { get; set; }
	public Guid ProfileId { get; set; }
	public Guid LikedProfileId { get; set; }
}
