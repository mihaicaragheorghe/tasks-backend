using FluentAssertions;

using MediatR;

using Moq;
using Application.Core;
using Application.UnitTests.TestUtils.Subtasks;
using Domain;
using Application.Common.ErrorHandling;
using Application.Subtasks.Commands;

namespace Application.UnitTests.Tasks.Commands;

public class DeleteSubtaskCommandHandlerTests
{
    private readonly Mock<ISubtaskRepository> _subtaskRepositoryMock;
    private readonly DeleteSubtaskCommandHandler _handler;

    public DeleteSubtaskCommandHandlerTests()
    {
        _subtaskRepositoryMock = new Mock<ISubtaskRepository>();
        _handler = new DeleteSubtaskCommandHandler(_subtaskRepositoryMock.Object);
    }
    
    [Fact]
    public async Task Handle_ShouldDeleteSubtask_WhenSubtaskExists()
    {
        // Arrange
        var subtask = SubtaskUtils.CreateSubtask();
        var command = new DeleteSubtaskCommand(subtask.Id);
        _subtaskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subtask);
        _subtaskRepositoryMock
            .Setup(x => x.DeleteAsync(subtask, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        _subtaskRepositoryMock.Verify(x => x.DeleteAsync(subtask, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenSubtaskDoesNotExist()
    {
        // Arrange
        var command = new DeleteSubtaskCommand(Guid.NewGuid());
        _subtaskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Subtask?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Subtask.NotFound.Description);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenFailedToDeleteSubtask()
    {
        // Arrange
        var subtask = SubtaskUtils.CreateSubtask();
        var command = new DeleteSubtaskCommand(subtask.Id);
        _subtaskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subtask);
        _subtaskRepositoryMock
            .Setup(x => x.DeleteAsync(subtask, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Subtask.FailedToDelete.Description);
    }
}