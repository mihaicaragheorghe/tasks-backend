using Tasks.Application.Comments.Commands;

namespace Tasks.Api.Contracts;

public record CreateCommentRequest(
    Guid TaskId,
    string Content)
{
    public CreateCommentCommand ToCommand(Guid userId)
        => new(TaskId, userId, Content);
}