using Tasks.Domain;

namespace Tasks.Application.UnitTests.TestUtils.Projects;

public static class ProjectUtils
{
    public static Project CreateProject(
        string name = "Test Project",
        Guid? ownerId = null,
        string? color = "#000000") =>
        Project.Create(
            name,
            ownerId ?? Guid.NewGuid(),
            color);
}