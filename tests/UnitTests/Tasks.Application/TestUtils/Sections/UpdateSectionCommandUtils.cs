using Application.Sections.Commands;

namespace Application.UnitTests.TestUtils.Sections;

public static class UpdateSectionCommandUtils
{
    public static UpdateSectionCommand CreateCommand(
        Guid id,
        string name = "Updated section",
        int order = 1) =>
        new(id, name, order);
}