using Application.Subtasks.Commands;

namespace Tasks.Api.Contracts;

public record CreateSubtaskRequest(Guid ParentId, string Title)
{
    public CreateSubtaskCommand ToCommand() => new(ParentId, Title);
};