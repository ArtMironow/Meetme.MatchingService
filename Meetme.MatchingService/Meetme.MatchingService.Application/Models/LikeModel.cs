namespace Meetme.MatchingService.Application.Models;

public class LikeModel
{
	public Guid ProfileId { get; set; }
	public Guid LikedProfileId { get; set; }
}
