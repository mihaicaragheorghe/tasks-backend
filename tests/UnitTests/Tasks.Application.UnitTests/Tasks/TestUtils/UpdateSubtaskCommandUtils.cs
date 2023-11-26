using Tasks.Application.Tasks.Commands;

namespace Tasks.Application.UnitTests.Tasks.TestUtils;

public class UpdateSubtaskCommandUtils
{
    public static UpdateSubtaskCommand CreateUpdateSubtaskCommand(
        Guid? id = null,
        string title = "Updated subtask",
        bool isCompleted = true) =>
        new(id ?? Guid.NewGuid(), title, isCompleted);
}