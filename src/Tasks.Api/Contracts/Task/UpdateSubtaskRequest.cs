using Application.Subtasks.Commands;

namespace Tasks.Api.Contracts;

public record UpdateSubtaskRequest(string Title, bool IsCompleted)
{
    public UpdateSubtaskCommand ToCommand(Guid id) => new(id, Title, IsCompleted);
};