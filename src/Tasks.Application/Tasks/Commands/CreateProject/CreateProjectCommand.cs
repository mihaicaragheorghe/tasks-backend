using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record CreateProjectCommand(
    Guid UserId,
    string Name,
    string? Color
) : IRequest<Project>;