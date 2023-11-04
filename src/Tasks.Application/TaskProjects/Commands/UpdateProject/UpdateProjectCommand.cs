using MediatR;

using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Commands;

public record UpdateProjectCommand(
    Guid Id,
    string Name,
    string? Color,
    int Order,
    bool IsArchived,
    bool IsFavorite) : IRequest<Project>;