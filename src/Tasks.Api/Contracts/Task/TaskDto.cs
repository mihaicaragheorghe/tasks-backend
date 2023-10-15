using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    TaskPriority Priority,
    bool IsComplete,
    DateTime CreatedAtUtc,
    DateTime? CompletedAtUtc,
    DateTime? DueAtUtc,
    Guid ProjectId,
    Guid? SectionId,
    UserProfileDto CreatedBy,
    UserProfileDto AssignedTo,
    ICollection<TagDto> Tags,
    ICollection<Subtask> Subtasks,
    ICollection<CommentDto> Comments)
{
    public TaskDto(TaskEntity task)
        : this(
            task.Id,
            task.Title,
            task.Description,
            task.Priority,
            task.IsComplete,
            task.CreatedAtUtc,
            task.CompletedAtUtc,
            task.DueAtUtc,
            task.ProjectId,
            task.SectionId,
            new UserProfileDto(task.CreatedBy),
            new UserProfileDto(task.AssignedTo),
            task.Tags
                .OrderBy(x => x.Name)
                .Select(x => new TagDto(x))
                .ToList(),
            task.Subtasks
                .OrderBy(x => x.CreatedAtUtc)
                .ToList(),
            task.Comments
                .OrderByDescending(x => x.CreatedAtUtc)
                .Select(x => new CommentDto(x))
                .ToList()) { }
}