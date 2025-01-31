using Meetme.MatchingService.Domain.DataTransferObjects.Enums;

namespace Meetme.MatchingService.Domain.DataTransferObjects;

public class ProfileDto
{
	public Guid Id { get; set; }
	public required string IdentityId { get; set; }
	public string? Name { get; set; }
	public int Age { get; set; }
	public string? Bio { get; set; }
	public Gender Gender { get; set; }
	public string? Location { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime UpdatedAt { get; set; }
	public PreferenceDto? Preference { get; set; }
	public ICollection<PhotoDto>? Photos { get; set; }
}
