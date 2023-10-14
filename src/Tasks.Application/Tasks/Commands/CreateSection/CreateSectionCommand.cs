using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record CreateSectionCommand(
    Guid ProjectId,
    string Name
) : IRequest<Section>;