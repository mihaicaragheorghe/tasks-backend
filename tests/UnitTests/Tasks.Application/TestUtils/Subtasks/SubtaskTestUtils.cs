using Domain;

namespace Application.UnitTests.TestUtils.Subtasks;

public static class SubtaskTestUtils
{
    public static Subtask CreateSubtask(Guid? taskId = null, string name = "Test subtask") =>
        Subtask.Create(taskId ?? Guid.NewGuid(), name);
}