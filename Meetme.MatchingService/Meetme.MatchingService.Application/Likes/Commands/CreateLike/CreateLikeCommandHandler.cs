using MapsterMapper;
using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Application.Models;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Commands.CreateLike;


public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, CreateLikeResult>
{
	private readonly IRepository<LikeEntity> _likeRepository;
	private readonly IMapper _mapper;
	private readonly IMatchService _matchService;

	public CreateLikeCommandHandler(IRepository<LikeEntity> likeRepository, IMapper mapper, IMatchService matchService)
	{
		_likeRepository = likeRepository;
		_mapper = mapper;
		_matchService = matchService;
	}

	public async Task<CreateLikeResult> Handle(CreateLikeCommand command, CancellationToken cancellationToken)
	{
		var likeEntity = _mapper.Map<LikeEntity>(command);

		await _likeRepository.AddAsync(likeEntity, cancellationToken);

		var likeModel = _mapper.Map<LikeModel>(likeEntity);
		await _matchService.MatchProfilesAsync(likeModel, cancellationToken);

		return new CreateLikeResult(likeEntity);
	}
}
