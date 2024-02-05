using Application.Tasks.Commands;

using Domain;

using FluentAssertions;

namespace Application.UnitTests.TestUtils.Tasks;

public static partial class TasksExtensions
{
    public static void ValidateCreatedFrom(this TaskEntity task, CreateTaskCommand command)
    {
        task.Id.Should().NotBeEmpty();
        task.ProjectId.Should().Be(command.ProjectId);
        task.SectionId.Should().Be(command.SectionId);
        task.AssignedToUserId.Should().Be(command.AssignedToUserId);
        task.CreatedByUserId.Should().Be(command.CreatedByUserId);
        task.Title.Should().Be(command.Title);
        task.Description.Should().Be(command.Description);
        task.Priority.Should().Be(command.Priority);
        task.DueAtUtc.Should().Be(command.DueAtUtc);
        task.Tags.Select(x => x.Id).Should().BeEquivalentTo(command.TagsIds);
        task.Subtasks.Select(x => x.Title).Should().BeEquivalentTo(command.SubtasksTitles);
        task.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    public static void ValidateUpdatedFrom(
        this TaskEntity task, 
        UpdateTaskCommand command,
        TaskEntity originalTask)
    {
        task.Id.Should().Be(originalTask.Id);
        task.CreatedAtUtc.Should().Be(originalTask.CreatedAtUtc);
        task.CreatedByUserId.Should().Be(originalTask.CreatedByUserId);
        task.ProjectId.Should().Be(command.ProjectId);
        task.SectionId.Should().Be(command.SectionId);
        task.AssignedToUserId.Should().Be(command.AssignedToUserId);
        task.Tags.Select(x => x.Id).Should().BeEquivalentTo(command.TagsIds);
        task.Title.Should().Be(command.Title);
        task.Description.Should().Be(command.Description);
        task.Priority.Should().Be(command.Priority);
        task.IsComplete.Should().Be(command.IsComplete);
        task.DueAtUtc.Should().Be(command.DueAtUtc);
        task.IsDeleted.Should().Be(command.IsDeleted);
        task.OrderIndex.Should().Be(command.OrderIndex);
    }
}