using MapsterMapper;
using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Application.Models;
using Meetme.MatchingService.Domain.Common.Exceptions;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Commands.DeleteLike;

public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand>
{
	private readonly IRepository<LikeEntity> _likeRepository;
	private readonly IMatchService _matchService;
	private readonly IMapper _mapper;

	public DeleteLikeCommandHandler(IRepository<LikeEntity> likeRepository, IMatchService matchService, IMapper mapper)
	{
		_likeRepository = likeRepository;
		_matchService = matchService;
		_mapper = mapper;
	}

	public async Task Handle(DeleteLikeCommand command, CancellationToken cancellationToken)
	{
		var likeEntity = await _likeRepository.GetByIdAsync(command.Id, cancellationToken);

		if (likeEntity == null)
		{
			throw new EntityNotFoundException("Like with this id does not exist");
		}

		await _likeRepository.RemoveAsync(likeEntity, cancellationToken);

		var likeModel = _mapper.Map<LikeModel>(likeEntity);
		await _matchService.RemoveMatchAsync(likeModel, cancellationToken);
	}
}
