using MediatR;
using Tasks.Domain;

namespace Tasks.Application.TaskSections.Commands;

public record CreateSectionCommand(
    Guid ProjectId,
    string Name
) : IRequest<Section>;