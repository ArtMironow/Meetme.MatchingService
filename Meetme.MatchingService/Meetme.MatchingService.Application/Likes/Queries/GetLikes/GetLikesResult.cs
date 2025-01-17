using Meetme.MatchingService.Domain.Entities;

namespace Meetme.MatchingService.Application.Likes.Queries.GetLikes;

public record GetLikesResult(IEnumerable<LikeEntity> Likes);
