namespace Meetme.MatchingService.Contracts.Likes.GetLikeById;

public record GetLikeByIdResponse(Guid Id, Guid ProfileId, Guid LikedProfileId);
