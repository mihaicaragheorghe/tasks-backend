using MediatR;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record UpdateTaskCommand(
    Guid Id,
    string Title,
    string? Description,
    TaskPriority Priority,
    bool IsComplete,
    DateTime? DueAtUtc,
    bool IsDeleted,
    int OrderIndex,
    Guid ProjectId,
    Guid? SectionId,
    Guid AssignedToUserId,
    List<Guid> TagsIds
) : IRequest<TaskEntity>;
