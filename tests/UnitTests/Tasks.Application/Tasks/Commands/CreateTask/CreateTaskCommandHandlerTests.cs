using Application.Core;
using Application.Tasks.Commands;
using Application.UnitTests.TestUtils.Tasks;
using Application.Common.ErrorHandling;

using Domain;

using FluentAssertions;

using Moq;

namespace Application.UnitTests.Tasks.Commands;

public class CreateTaskCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<ITagRepository> _tagRepositoryMock;

    public CreateTaskCommandHandlerTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _tagRepositoryMock = new Mock<ITagRepository>();
    }

    [Fact]
    public async Task Handle_ShouldReturnTask_WhenTaskCreated()
    {
        // Arrange
        var handler = new CreateTaskCommandHandler(_taskRepositoryMock.Object, _tagRepositoryMock.Object);
        var command = CreateTaskCommandUtils.CreateCommand();
        _tagRepositoryMock
            .Setup(x => x.GetTagsByIdsAsync(It.IsAny<List<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        _taskRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.ValidateCreatedFrom(command);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenTaskNotCreated()
    {
        // Arrange
        var handler = new CreateTaskCommandHandler(_taskRepositoryMock.Object, _tagRepositoryMock.Object);
        var command = CreateTaskCommandUtils.CreateCommand();
        _tagRepositoryMock
            .Setup(x => x.GetTagsByIdsAsync(It.IsAny<List<Guid>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);
        _taskRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .WithMessage(Errors.Task.FailedToCreate.Description);
    }
}