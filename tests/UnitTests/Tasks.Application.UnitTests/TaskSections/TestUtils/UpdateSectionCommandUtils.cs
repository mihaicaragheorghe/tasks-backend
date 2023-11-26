using Tasks.Application.TaskSections.Commands;

namespace Tasks.Application.UnitTests.TaskSections.TestUtils;

public static class UpdateSectionCommandUtils
{
    public static UpdateSectionCommand CreateCommand(
        Guid id,
        string name = "Updated section",
        int order = 1) =>
        new(id, name, order);
}