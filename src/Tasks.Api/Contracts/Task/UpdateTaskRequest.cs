using Tasks.Domain;

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
    List<Guid>? TagsIds);