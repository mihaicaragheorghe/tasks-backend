using Application.Sections.Commands;

namespace Application.UnitTests.TestUtils.Sections;

public static class CreateSectionCommandUtils
{
    public static CreateSectionCommand CreateCommand(
        Guid? projectId = null,
        string name = "Test section") =>
        new(projectId ?? Guid.NewGuid(), name);
}