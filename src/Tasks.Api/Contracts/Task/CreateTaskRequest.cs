using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record CreateTaskRequest(
    Guid ProjectId,
    Guid SectionId,
    Guid AssignedToUserId,
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? DueAtUtc,
    List<string>? SubtasksTitles,
    List<Guid>? TagsIds);