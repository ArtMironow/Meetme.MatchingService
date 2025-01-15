namespace Meetme.MatchingService.Domain.Entities;

public class MatchEntity
{
	public Guid MatchId { get; set; }
	public Guid ProfileId { get; set; }
	public Guid MatchedProfileId { get; set; }
	public DateTime MatchDate { get; set; }
}
