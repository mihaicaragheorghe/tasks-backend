using MediatR;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record UpdateSectionCommand(
    Guid Id,
    string Name,
    int OrderIndex) : IRequest<Section>;