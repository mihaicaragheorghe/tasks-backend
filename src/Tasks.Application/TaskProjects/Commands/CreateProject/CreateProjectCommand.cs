using MediatR;
using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Commands;

public record CreateProjectCommand(
    Guid UserId,
    string Name,
    string? Color
) : IRequest<Project>;