namespace Meetme.MatchingService.Contracts.Likes.CreateLike;

public record CreateLikeResponse(Guid Id, Guid ProfileId, Guid LikedProfileId);
