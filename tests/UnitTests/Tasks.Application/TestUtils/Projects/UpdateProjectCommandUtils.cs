using Application.Projects.Commands;

namespace Application.UnitTests.TestUtils.Projects;

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