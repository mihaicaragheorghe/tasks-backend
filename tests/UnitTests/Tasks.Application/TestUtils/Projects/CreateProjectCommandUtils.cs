using Application.Projects.Commands;

namespace Application.UnitTests.TestUtils.Projects;

public static class CreateProjectCommandUtils
{
    public static CreateProjectCommand CreateProject(
        string name = "Test Project",
        string color = "#000000",
        Guid? userId = null) =>
        new(Name: name, Color: color, UserId: userId ?? Guid.NewGuid());
}