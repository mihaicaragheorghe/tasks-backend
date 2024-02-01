using FluentAssertions;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.UnitTests.TestUtils.Subtasks;
using Domain;
using Application.Subtasks.Commands;

namespace Application.UnitTests.Subtasks.Commands;

public class CreateSubtaskCommandHandlerTests
{
    private readonly Mock<ISubtaskRepository> _subtaskRepositoryMock;
    private readonly CreateSubtaskCommandHandler _handler;

    public CreateSubtaskCommandHandlerTests()
    {
        _subtaskRepositoryMock = new Mock<ISubtaskRepository>();
        _handler = new CreateSubtaskCommandHandler(_subtaskRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSubtask_WhenSubtaskCreated()
    {
        // Arrange
        var command = CreateSubtaskCommandUtils.CreateCommand();
        _subtaskRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Subtask>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ValidateCreatedFrom(command);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenRepositoryFails()
    {
        // Arrange
        var command = CreateSubtaskCommandUtils.CreateCommand();
        _subtaskRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Subtask>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Subtask.FailedToCreate.Description);
    }
}