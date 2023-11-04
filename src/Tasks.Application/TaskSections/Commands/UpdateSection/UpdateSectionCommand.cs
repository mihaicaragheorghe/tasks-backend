using MediatR;

using Tasks.Domain;

namespace Tasks.Application.TaskSections.Commands;

public record UpdateSectionCommand(
    Guid Id,
    string Name,
    int OrderIndex) : IRequest<Section>;