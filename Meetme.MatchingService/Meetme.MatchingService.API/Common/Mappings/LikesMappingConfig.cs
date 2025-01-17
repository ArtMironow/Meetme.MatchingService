using Mapster;
using Meetme.MatchingService.Application.Likes.Commands.CreateLike;
using Meetme.MatchingService.Application.Likes.Queries.GetLikeById;
using Meetme.MatchingService.Application.Likes.Queries.GetLikes;
using Meetme.MatchingService.Contracts.Likes.CreateLike;
using Meetme.MatchingService.Contracts.Likes.GetLikeById;
using Meetme.MatchingService.Contracts.Likes.GetLikes;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.API.Common.Mappings;

public class LikesMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateLikeRequest, CreateLikeCommand>();

		config.NewConfig<CreateLikeResult, CreateLikeResponse>()
			.Map(dest => dest, src => src.Like);

		config.NewConfig<LikeEntity, GetLikesResponse>();

		config.NewConfig<GetLikesResult, IEnumerable<GetLikesResponse>>()
			.MapWith(src => src.Likes.Select(like => like.Adapt<GetLikesResponse>()));

		config.NewConfig<GetLikeByIdResult, GetLikeByIdResponse>()
			.Map(dest => dest, src => src.Like);
	}
}
