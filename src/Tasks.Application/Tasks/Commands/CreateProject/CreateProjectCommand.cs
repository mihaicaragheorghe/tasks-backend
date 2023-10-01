using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks;

public record CreateProjectCommand(
    Guid UserId,
    string Name,
    string? Color
) : IRequest<Project>;