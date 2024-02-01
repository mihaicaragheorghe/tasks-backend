using MediatR;

using Domain;

namespace Application.Projects.Commands;

public record UpdateProjectCommand(
    Guid Id,
    string Name,
    string? Color,
    int Order,
    bool IsArchived,
    bool IsFavorite) : IRequest<Project>;