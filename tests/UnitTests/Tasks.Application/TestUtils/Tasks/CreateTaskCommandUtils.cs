using Application.Tasks.Commands;

using Domain;

namespace Application.UnitTests.TestUtils.Tasks;

public static class CreateTaskCommandUtils
{
    public static CreateTaskCommand CreateCommand(
        Guid? projectId = null,
        Guid? sectionId = null,
        Guid? assignedToUserId = null,
        Guid? createdByUserId = null,
        string title = "Test task",
        string? description = null,
        TaskPriority priority = TaskPriority.Medium,
        DateTime? dueAtUtc = null,
        List<Guid>? tagsIds = null,
        List<string>? subtasksTitles = null) =>
            new(projectId ?? Guid.NewGuid(),
                sectionId ?? Guid.NewGuid(),
                assignedToUserId ?? Guid.NewGuid(),
                createdByUserId ?? Guid.NewGuid(),
                title,
                description,
                priority,
                dueAtUtc,
                tagsIds ?? new List<Guid>(),
                subtasksTitles ?? new List<string>());
}