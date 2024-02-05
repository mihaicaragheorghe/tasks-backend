using Application.Tasks.Commands;

using Domain;

namespace Application.UnitTests.TestUtils.Tasks;

public static class UpdateTaskCommandUtils
{
    public static UpdateTaskCommand CreateCommand(
        Guid? id = null,
        string title = "Updated task",
        string? description = "Updated description",
        TaskPriority priority = TaskPriority.High,
        bool isComplete = true,
        DateTime? dueAtUtc = null,
        bool isDeleted = false,
        int orderIndex = 1,
        Guid? projectId = null,
        Guid? sectionId = null,
        Guid? assignedToUserId = null,
        List<Guid>? tagsIds = null) =>
        new(id ?? Guid.NewGuid(),
            title,
            description,
            priority,
            isComplete,
            dueAtUtc,
            isDeleted,
            orderIndex,
            projectId ?? Guid.NewGuid(),
            sectionId ?? Guid.NewGuid(),
            assignedToUserId ?? Guid.NewGuid(),
            tagsIds ?? new List<Guid>());
}