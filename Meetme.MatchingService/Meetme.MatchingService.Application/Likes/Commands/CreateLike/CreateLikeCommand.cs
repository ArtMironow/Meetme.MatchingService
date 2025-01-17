using MediatR;

namespace Meetme.MatchingService.Application.Likes.Commands.CreateLike;

public record CreateLikeCommand(Guid ProfileId, Guid LikedProfileId) : IRequest<CreateLikeResult>;
