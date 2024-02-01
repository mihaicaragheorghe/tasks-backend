using MediatR;

using Domain;

namespace Application.Sections.Commands;

public record UpdateSectionCommand(
    Guid Id,
    string Name,
    int OrderIndex) : IRequest<Section>;