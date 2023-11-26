using FluentAssertions;

using Tasks.Application.TaskProjects.Commands;
using Tasks.Domain;

namespace Tasks.Application.UnitTests.TestUtils.Projects;

public static partial class ProjectExtensions
{
    public static void ValidateCreatedFrom(this Project project, CreateProjectCommand command)
    {
        project.Name.Should().Be(command.Name);
        project.Color.Should().Be(command.Color);
        project.OwnerId.Should().Be(command.UserId);
        project.Id.Should().NotBeEmpty();
        project.OrderIndex.Should().Be(0);
        project.Sections.Should().BeEmpty();
        project.Tasks.Should().BeEmpty();
        project.Collaborators.Should().BeEmpty();
        project.IsArchived.Should().BeFalse();
        project.IsFavorite.Should().BeFalse();
        project.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    public static void ValidateUpdatedFrom(
        this Project project, 
        Project originalProject, 
        UpdateProjectCommand command)
    {
        project.Id.Should().Be(originalProject.Id);
        project.CreatedAtUtc.Should().Be(originalProject.CreatedAtUtc);
        project.OwnerId.Should().Be(originalProject.OwnerId);
        project.Sections.Should().BeEquivalentTo(originalProject.Sections);
        project.Tasks.Should().BeEquivalentTo(originalProject.Tasks);
        project.Collaborators.Should().BeEquivalentTo(originalProject.Collaborators);

        project.Name.Should().Be(command.Name);
        project.Color.Should().Be(command.Color);
        project.OrderIndex.Should().Be(command.Order);
        project.IsArchived.Should().Be(command.IsArchived);
        project.IsFavorite.Should().Be(command.IsFavorite);
    }
}