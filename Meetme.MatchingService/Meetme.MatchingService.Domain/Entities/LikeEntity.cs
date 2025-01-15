namespace Meetme.MatchingService.Domain.Entities;

public class LikeEntity
{
	public Guid LikeId { get; set; }
	public Guid ProfileId { get; set; }
	public Guid LikedProfileId { get; set; }
}
