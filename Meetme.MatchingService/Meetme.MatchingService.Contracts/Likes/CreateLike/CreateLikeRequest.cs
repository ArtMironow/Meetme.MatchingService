namespace Meetme.MatchingService.Contracts.Likes.CreateLike;

public record CreateLikeRequest(Guid ProfileId, Guid LikedProfileId);
