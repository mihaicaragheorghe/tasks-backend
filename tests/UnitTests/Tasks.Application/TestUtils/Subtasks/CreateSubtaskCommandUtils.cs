using Application.Subtasks.Commands;

namespace Application.UnitTests.TestUtils.Subtasks;

public static class CreateSubtaskCommandUtils
{
    public static CreateSubtaskCommand CreateCommand(
        Guid? parentId = null,
        string title = "Test subtask") =>
        new(parentId ?? Guid.NewGuid(), title);
}