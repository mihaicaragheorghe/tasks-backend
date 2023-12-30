using Tasks.Application.Tasks.Commands;

namespace Tasks.Application.UnitTests.Tasks.TestUtils
{
    public static class CreateSubtaskCommandUtils
    {
        public static CreateSubtaskCommand CreateCommand(
            Guid? parentId = null,
            string title = "Test subtask") =>
            new(parentId ?? Guid.NewGuid(), title);
    }
}