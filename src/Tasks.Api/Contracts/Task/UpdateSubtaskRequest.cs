using Tasks.Application.Tasks.Commands;

namespace Tasks.Api.Contracts;

public record UpdateSubtaskRequest(string Title, bool IsCompleted)
{
    public UpdateSubtaskCommand ToCommand(Guid id) => new(id, Title, IsCompleted);
};