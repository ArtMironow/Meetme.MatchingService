using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.Common.Exceptions;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Commands.DeleteLike;

public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
{
	private readonly IRepository<LikeEntity> _likeRepository;
	private readonly IMatchService _matchService;

	public DeleteLikeCommandHandler(IRepository<LikeEntity> likeRepository, IMatchService matchService)
	{
		_likeRepository = likeRepository;
		_matchService = matchService;
	}

	public async Task Handle(DeleteLikeCommand command, CancellationToken cancellationToken)
	{
		var likeEntity = await _likeRepository.GetByIdAsync(command.Id, cancellationToken);

		if (likeEntity == null)
		{
			throw new EntityNotFoundException("Like with this id does not exist");
		}

		await _likeRepository.RemoveAsync(likeEntity, cancellationToken);

		await _matchService.RemoveMatchAsync(likeEntity, cancellationToken);
	}
}
