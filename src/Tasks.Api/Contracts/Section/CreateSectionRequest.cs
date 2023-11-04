using Tasks.Application.Tasks.Commands;

namespace Tasks.Api.Contracts;

public record CreateSectionRequest(
    Guid ProjectId,
    string Name)
{
    public CreateSectionCommand ToCommand() => new(
        ProjectId,
        Name);
}