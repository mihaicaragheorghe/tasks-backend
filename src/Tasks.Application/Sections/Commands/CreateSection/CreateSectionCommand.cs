using MediatR;
using Domain;

namespace Application.Sections.Commands;

public record CreateSectionCommand(
    Guid ProjectId,
    string Name
) : IRequest<Section>;