using Meetme.MatchingService.Domain.DataTransferObjects;

namespace Meetme.MatchingService.Application.Common.Interfaces;

public interface IProfileServiceClient
{
	Task<ProfileDto?> GetProfileAsync(Guid id);
}
