using Application.Tasks.Commands;
using Domain;

namespace Tasks.Api.Contracts;

public record UpdateTaskRequest(
    string Title,
    string Description,
    TaskPriority Priority,
    bool IsComplete,
    DateTime? DueAtUtc,
    bool IsDeleted,
    int OrderIndex,
    Guid ProjectId,
    Guid? SectionId,
    Guid AssignedToUserId,
    List<Guid>? TagsIds)
{
    public UpdateTaskCommand ToCommand(Guid id) => new(
        id,
        Title,
        Description,
        Priority,
        IsComplete,
        DueAtUtc,
        IsDeleted,
        OrderIndex,
        ProjectId,
        SectionId,
        AssignedToUserId,
        TagsIds ?? new List<Guid>());
};