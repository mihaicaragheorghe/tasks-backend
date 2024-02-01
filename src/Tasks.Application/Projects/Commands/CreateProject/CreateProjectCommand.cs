using MediatR;
using Domain;

namespace Application.Projects.Commands;

public record CreateProjectCommand(
    Guid UserId,
    string Name,
    string? Color
) : IRequest<Project>;