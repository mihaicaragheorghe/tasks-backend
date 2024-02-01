using MediatR;

using Domain;

namespace Application.Comments.Commands;

public record CreateCommentCommand(Guid TaskId, Guid UserId, string Content) 
    : IRequest<Comment>;
