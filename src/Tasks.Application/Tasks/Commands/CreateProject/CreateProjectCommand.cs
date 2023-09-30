using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks;

public record CreateProjectCommand(
    string Name,
    string Color
) : IRequest<TaskProject>;