using Tasks.Application.TaskProjects.Commands;

namespace Tasks.Api.Contracts;

public record UpdateProjectRequest(
    string Name,
    string? Color,
    int Order,
    bool IsArchived,
    bool IsFavorite)
{
    public UpdateProjectCommand ToCommand(Guid id) => new(
        id,
        Name,
        Color,
        Order,
        IsArchived,
        IsFavorite);
};