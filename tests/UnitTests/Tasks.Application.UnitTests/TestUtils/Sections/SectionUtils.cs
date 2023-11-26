using Tasks.Domain;

namespace Tasks.Application.UnitTests.TestUtils.Sections;

public class SectionUtils
{
    public static Section CreateSection(
        Guid? projectId = null,
        string name = "Test section") =>
        Section.Create(projectId ?? Guid.NewGuid(), name);

    public static List<Section> CreateCollection(
        int count = 1,
        Guid? projectId = null) =>
        Enumerable.Range(0, count)
            .Select(index => CreateSection(projectId, $"Test section {index}"))
            .ToList();
}