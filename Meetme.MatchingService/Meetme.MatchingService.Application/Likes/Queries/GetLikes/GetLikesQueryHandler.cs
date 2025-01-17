using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Queries.GetLikes;

public class GetLikesQueryHandler : IRequestHandler<GetLikesQuery, GetLikesResult>
{
	private readonly IRepository<LikeEntity> _likeRepository;

	public GetLikesQueryHandler(IRepository<LikeEntity> likeRepository)
	{
		_likeRepository = likeRepository;
	}

	public async Task<GetLikesResult> Handle(GetLikesQuery request, CancellationToken cancellationToken)
	{
		var likeEntities = await _likeRepository.GetAllAsync(cancellationToken);

		return new GetLikesResult(likeEntities);
	}
}
