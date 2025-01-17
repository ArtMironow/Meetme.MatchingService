using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.Common.Exceptions;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Queries.GetLikeById;

public class GetLikeByIdQueryHandler : IRequestHandler<GetLikeByIdQuery, GetLikeByIdResult>
{
	private readonly IRepository<LikeEntity> _likeRepository;

	public GetLikeByIdQueryHandler(IRepository<LikeEntity> likeRepository)
	{
		_likeRepository = likeRepository;
	}

	public async Task<GetLikeByIdResult> Handle(GetLikeByIdQuery query, CancellationToken cancellationToken)
	{
		var likeEntity = await _likeRepository.GetByIdAsync(query.Id, cancellationToken);

		if (likeEntity == null)
		{
			throw new EntityNotFoundException("Like with this id does not exist");
		}

		return new GetLikeByIdResult(likeEntity);
	}
}
