using MediatR;

namespace Meetme.MatchingService.Application.Likes.Commands.DeleteLike;

public record DeleteLikeCommand(Guid Id) : IRequest;
