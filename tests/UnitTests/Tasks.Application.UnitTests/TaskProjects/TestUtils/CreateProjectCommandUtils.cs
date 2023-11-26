using Tasks.Application.TaskProjects.Commands;

namespace Tasks.Application.UnitTests.TaskProjects.TestUtils
{
    public static class CreateProjectCommandUtils
    {
        public static CreateProjectCommand CreateProject(
            string name = "Test Project",
            string color = "#000000",
            Guid? userId = null) =>
            new CreateProjectCommand(
                Name: name, 
                Color: color, 
                UserId: userId ?? Guid.NewGuid());
    }
}