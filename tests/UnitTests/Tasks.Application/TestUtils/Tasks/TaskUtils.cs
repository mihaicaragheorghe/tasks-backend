using Domain;

namespace Application.UnitTests.TestUtils.Tasks;

public static class TaskUtils
{
    public static TaskEntity CreateTask(
        Guid? createdByUserId = null,
        Guid? assignedToUserId = null,
        Guid? projectId = null,
        Guid? sectionId = null,
        string title = "Test task",
        string? description = null,
        TaskPriority priority = TaskPriority.Medium,
        DateTime? dueAtUtc = null) =>
        TaskEntity.Create(
            createdByUserId ?? Guid.NewGuid(),
            assignedToUserId ?? Guid.NewGuid(),
            projectId ?? Guid.NewGuid(),
            sectionId ?? Guid.NewGuid(),
            title,
            description,
            priority,
            dueAtUtc);

    public static List<TaskEntity> CreateTasks(
        int count = 1,
        Guid? createdByUserId = null,
        Guid? assignedToUserId = null,
        Guid? projectId = null,
        Guid? sectionId = null) =>
        Enumerable.Range(0, count)
            .Select(i => CreateTask(
                createdByUserId: createdByUserId, 
                assignedToUserId: assignedToUserId, 
                projectId: projectId, 
                sectionId: sectionId, 
                title: $"Test task {i}"))
            .ToList();
}