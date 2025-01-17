using MediatR;

namespace Meetme.MatchingService.Application.Likes.Queries.GetLikes;

public record GetLikesQuery() : IRequest<GetLikesResult>;
