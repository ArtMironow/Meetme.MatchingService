using Mapster;
using Meetme.MatchingService.Application.Likes.Commands.CreateLike;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Common.Mappings;

public class LikesMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateLikeCommand, LikeEntity>();
	}
}
