using Tasks.Application.TaskSections.Commands;

namespace Tasks.Application.UnitTests.TaskSections.TestUtils
{
    public static class CreateSectionCommandUtils
    {
        public static CreateSectionCommand CreateCommand(
            Guid? projectId = null,
            string name = "Test section") =>
            new(projectId ?? Guid.NewGuid(), name);
    }
}