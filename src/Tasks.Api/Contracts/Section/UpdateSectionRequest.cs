using Tasks.Application.TaskSections.Commands;

namespace Tasks.Api.Contracts;

public record UpdateSectionRequest(
    string Name,
    int OrderIndex)
{
    public UpdateSectionCommand ToCommand(Guid id) => new(id, Name, OrderIndex);
};