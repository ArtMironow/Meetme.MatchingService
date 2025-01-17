namespace Meetme.MatchingService.Contracts.Likes.GetLikes;

public record GetLikesResponse(Guid Id, Guid ProfileId, Guid LikedProfileId);
