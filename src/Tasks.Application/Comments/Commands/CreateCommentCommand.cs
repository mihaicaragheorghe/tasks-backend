using MediatR;

using Tasks.Domain;

namespace Tasks.Application.Comments.Commands;

public record CreateCommentCommand(Guid TaskId, Guid UserId, string Content) 
    : IRequest<Comment>;
