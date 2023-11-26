using Tasks.Application.Tasks.Commands;

namespace Tasks.Application.UnitTests.Tasks.TestUtils
{
    public static class CreateSubtaskCommandUtils
    {
        public static CreateSubtaskCommand CreateSubtaskCommand(
            Guid? parentId = null,
            string title = "Test subtask") =>
            new(parentId ?? Guid.NewGuid(), title);
    }
}