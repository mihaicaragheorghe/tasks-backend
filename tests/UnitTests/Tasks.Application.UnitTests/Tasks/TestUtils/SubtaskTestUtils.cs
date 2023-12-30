using Tasks.Domain;

namespace Tasks.Application.UnitTests.Tasks.TestUtils
{
    public static class SubtaskTestUtils
    {
        public static Subtask CreateSubtask(Guid? taskId = null, string name = "Test subtask") =>
            Subtask.Create(taskId ?? Guid.NewGuid(), name);
    }
}