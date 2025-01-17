using MapsterMapper;
using MediatR;
using Meetme.MatchingService.Application.Likes.Commands.CreateLike;
using Meetme.MatchingService.Application.Likes.Commands.DeleteLike;
using Meetme.MatchingService.Application.Likes.Queries.GetLikeById;
using Meetme.MatchingService.Application.Likes.Queries.GetLikes;
using Meetme.MatchingService.Contracts.Likes.CreateLike;
using Meetme.MatchingService.Contracts.Likes.GetLikeById;
using Meetme.MatchingService.Contracts.Likes.GetLikes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meetme.MatchingService.API.Controllers;


[ApiController]
[Authorize]
[Route("[controller]")]
public class LikesController : ControllerBase
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public LikesController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<IEnumerable<GetLikesResponse>> GetAllAsync(CancellationToken cancellationToken)
	{
		var getLikesQuery = new GetLikesQuery();

		var getLikesResult = await _mediator.Send(getLikesQuery, cancellationToken);

		var getLikesResponse = _mapper.Map<IEnumerable<GetLikesResponse>>(getLikesResult);

		return getLikesResponse;
	}

	[HttpGet("{id}")]
	public async Task<GetLikeByIdResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
	{
		var getLikeByIdQuery = new GetLikeByIdQuery(id);

		var getLikeByIdResult = await _mediator.Send(getLikeByIdQuery, cancellationToken);

		var getLikeByIdResponse = _mapper.Map<GetLikeByIdResponse>(getLikeByIdResult);

		return getLikeByIdResponse;
	}

	[HttpPost]
	public async Task<CreateLikeResponse> CreateAsync(CreateLikeRequest request, CancellationToken cancellationToken)
	{
		var createLikeCommand = _mapper.Map<CreateLikeCommand>(request);

		var createLikeResult = await _mediator.Send(createLikeCommand, cancellationToken);

		var createLikeResponse = _mapper.Map<CreateLikeResponse>(createLikeResult);

		return createLikeResponse;
	}

	[HttpDelete("{id}")]
	public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
	{
		var deleteLikeCommand = new DeleteLikeCommand(id);

		return _mediator.Send(deleteLikeCommand, cancellationToken);
	}
}
