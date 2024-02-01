using Application.Subtasks.Commands;

namespace Application.UnitTests.TestUtils.Subtasks;

public class UpdateSubtaskCommandUtils
{
    public static UpdateSubtaskCommand CreateUpdateSubtaskCommand(
        Guid? id = null,
        string title = "Updated subtask",
        bool isCompleted = true) =>
        new(id ?? Guid.NewGuid(), title, isCompleted);
}