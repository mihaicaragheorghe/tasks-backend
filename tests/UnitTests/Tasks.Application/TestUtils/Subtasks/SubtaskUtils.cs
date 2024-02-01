using Domain;

namespace Application.UnitTests.TestUtils.Subtasks;

public static class SubtaskUtils
{
    public static Subtask CreateSubtask(
        Guid? parentId = null,
        string title = "Test subtask") =>
        Subtask.Create(parentId ?? Guid.NewGuid(), title);

    public static List<Subtask> CreateSubtasks(
        int count = 1,
        Guid? parentId = null) =>
        Enumerable.Range(0, count)
            .Select(i => CreateSubtask(parentId, $"Test subtask {i}"))
            .ToList();
}