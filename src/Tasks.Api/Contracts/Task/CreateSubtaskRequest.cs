using Tasks.Application.Tasks.Commands;

namespace Tasks.Api.Contracts;

public record CreateSubtaskRequest(Guid ParentId, string Title)
{
    public CreateSubtaskCommand ToCommand() => new(ParentId, Title);
};