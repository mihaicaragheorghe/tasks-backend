using FluentAssertions;

using Tasks.Application.Tasks.Commands;
using Tasks.Domain;

namespace Tasks.Application.UnitTests.TestUtils.Subtasks;

public static partial class SubtasksExtensions
{
    public static void ValidateCreatedFrom(this Subtask createdSubtask, CreateSubtaskCommand command)
    {
        createdSubtask.Id.Should().NotBeEmpty();
        createdSubtask.ParentId.Should().Be(command.ParentId);
        createdSubtask.Title.Should().Be(command.Title);
        createdSubtask.IsComplete.Should().BeFalse();
        createdSubtask.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMinutes(1));
    }

    public static void ValidateUpdatedFrom(
        this Subtask updatedSubtask, 
        Subtask originalSubtask, 
        UpdateSubtaskCommand command)
    {
        updatedSubtask.Id.Should().Be(originalSubtask.Id);
        updatedSubtask.ParentId.Should().Be(originalSubtask.ParentId);
        updatedSubtask.Title.Should().Be(command.Title);
        updatedSubtask.IsComplete.Should().Be(command.IsCompleted);
        updatedSubtask.CreatedAtUtc.Should().Be(originalSubtask.CreatedAtUtc);
    }
}