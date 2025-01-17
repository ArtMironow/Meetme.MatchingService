using MediatR;

namespace Meetme.MatchingService.Application.Likes.Queries.GetLikeById;

public record GetLikeByIdQuery(Guid Id) : IRequest<GetLikeByIdResult>;
