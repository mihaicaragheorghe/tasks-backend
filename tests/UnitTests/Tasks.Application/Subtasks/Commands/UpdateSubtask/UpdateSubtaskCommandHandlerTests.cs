using FluentAssertions;

using Moq;

using Application.Core;
using Application.UnitTests.TestUtils.Subtasks;
using Application.Common.ErrorHandling;
using Domain;
using Application.Subtasks.Commands;

namespace Application.UnitTests.Tasks.Commands;

public class UpdateSubtaskCommandHandlerTests
{
    private readonly Mock<ISubtaskRepository> _subtaskRepositoryMock;
    private readonly UpdateSubtaskCommandHandler _handler;

    public UpdateSubtaskCommandHandlerTests()
    {
        _subtaskRepositoryMock = new Mock<ISubtaskRepository>();
        _handler = new UpdateSubtaskCommandHandler(_subtaskRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateSubtask_WhenSubtaskExists()
    {
        // Arrange
        var subtask = SubtaskUtils.CreateSubtask();
        var command = UpdateSubtaskCommandUtils.CreateUpdateSubtaskCommand();
        _subtaskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subtask);
        _subtaskRepositoryMock
            .Setup(x => x.UpdateAsync(subtask, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ValidateUpdatedFrom(subtask, command);
        _subtaskRepositoryMock.Verify(x => x.UpdateAsync(subtask, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenSubtaskDoesNotExist()
    {
        // Arrange
        var command = UpdateSubtaskCommandUtils.CreateUpdateSubtaskCommand();
        _subtaskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Subtask?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Subtask.NotFound.Description);
        _subtaskRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Subtask>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenFailedToUpdateSubtask()
    {
        // Arrange
        var subtask = SubtaskUtils.CreateSubtask();
        var command = UpdateSubtaskCommandUtils.CreateUpdateSubtaskCommand();
        _subtaskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subtask);
        _subtaskRepositoryMock
            .Setup(x => x.UpdateAsync(subtask, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Subtask.FailedToUpdate.Description);
        _subtaskRepositoryMock.Verify(x => x.UpdateAsync(subtask, It.IsAny<CancellationToken>()), Times.Once);
    }
}