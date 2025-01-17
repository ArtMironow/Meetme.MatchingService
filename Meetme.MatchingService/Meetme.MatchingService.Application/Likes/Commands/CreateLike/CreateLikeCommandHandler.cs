using MapsterMapper;
using MediatR;
using Meetme.MatchingService.Application.Common.Interfaces;
using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Commands.CreateLike;


public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, CreateLikeResult>
{
	private readonly IRepository<LikeEntity> _likeRepository;
	private readonly IMapper _mapper;

	public CreateLikeCommandHandler(IRepository<LikeEntity> likeRepository, IMapper mapper)
	{
		_likeRepository = likeRepository;
		_mapper = mapper;
	}

	public async Task<CreateLikeResult> Handle(CreateLikeCommand command, CancellationToken cancellationToken)
	{
		var likeEntity = _mapper.Map<LikeEntity>(command);

		await _likeRepository.AddAsync(likeEntity, cancellationToken);

		return new CreateLikeResult(likeEntity);
	}
}
