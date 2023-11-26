using Tasks.Application.TaskProjects.Commands;

namespace Tasks.Application.UnitTests.TaskProjects.TestUtils;

public class UpdateProjectCommandUtils
{
    public static UpdateProjectCommand CreateCommand(
        Guid id,
        string name = "Updated Project",
        string? color = "#FFFFFF",
        int order = 1,
        bool isArchived = true,
        bool isFavorite = true) =>
        new UpdateProjectCommand(
            Id: id,
            Name: name,
            Color: color,
            Order: order,
            IsArchived: isArchived,
            IsFavorite: isFavorite);
}