using Application.Projects.Commands;

namespace Tasks.Api.Contracts;

public record CreateProjectRequest(
    string Name,
    string? Color)
{
    public CreateProjectCommand ToCommand(Guid userId) => new(
        userId,
        Name,
        Color);
};