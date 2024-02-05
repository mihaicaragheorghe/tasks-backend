using Application.Core;
using Application.Tasks.Commands;
using Application.UnitTests.TestUtils.Projects;
using Application.UnitTests.TestUtils.Tasks;
using Application.Common.ErrorHandling;

using Domain;

using FluentAssertions;

using Moq;
using Application.UnitTests.TestUtils.Sections;

namespace Application.UnitTests.Tasks.Commands;

public class UpdateTaskCommandHandlerTests
{
    private readonly Mock<ITaskRepository> _taskRepositoryMock;
    private readonly Mock<ITagRepository> _tagRepositoryMock;
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<ISectionRepository> _sectionRepositoryMock;
    private readonly UpdateTaskCommandHandler _handler;

    public UpdateTaskCommandHandlerTests()
    {
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _tagRepositoryMock = new Mock<ITagRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _sectionRepositoryMock = new Mock<ISectionRepository>();

        _handler = new UpdateTaskCommandHandler(
            _taskRepositoryMock.Object,
            _projectRepositoryMock.Object,
            _sectionRepositoryMock.Object,
            _tagRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTask_WhenTaskUpdated()
    {
        // Arrange
        var originalTask = TaskUtils.CreateTask();
        var command = UpdateTaskCommandUtils.CreateCommand(
            id: originalTask.Id,
            projectId: originalTask.ProjectId,
            sectionId: originalTask.SectionId,
            tagsIds: originalTask.Tags.Select(x => x.Id).ToList());
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(originalTask.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(originalTask);
        _taskRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        _tagRepositoryMock
            .Setup(x => x.GetTagsByIdsAsync(command.TagsIds, It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ValidateUpdatedFrom(command, originalTask);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenTaskNotFound()
    {
        // Arrange
        var command = UpdateTaskCommandUtils.CreateCommand();
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((TaskEntity?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .WithMessage(Errors.Task.NotFound.Description);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenProjectNotFound()
    {
        // Arrange
        var command = UpdateTaskCommandUtils.CreateCommand();
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(TaskUtils.CreateTask());
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(command.ProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .WithMessage(Errors.Project.NotFound.Description);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenSectionNotFound()
    {
        // Arrange
        var command = UpdateTaskCommandUtils.CreateCommand();
        var task = TaskUtils.CreateTask();
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(command.ProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ProjectUtils.CreateProject());
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(command.SectionId!.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Section?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .WithMessage(Errors.Section.NotFound.Description);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenSectionNotInProject()
    {
        // Arrange
        var command = UpdateTaskCommandUtils.CreateCommand();
        var task = TaskUtils.CreateTask();
        var section = SectionUtils.CreateSection();
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(command.ProjectId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ProjectUtils.CreateProject());
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(command.SectionId!.Value, It.IsAny<CancellationToken>()))
            .ReturnsAsync(section);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .WithMessage(Errors.Section.SectionNotInProject.Description);
    }

    [Fact]
    public async Task Handle_ShouldThrowServiceException_WhenTaskNotUpdated()
    {
        // Arrange
        var task = TaskUtils.CreateTask();
        var command = UpdateTaskCommandUtils.CreateCommand(
            id: task.Id,
            projectId: task.ProjectId,
            sectionId: task.SectionId,
            tagsIds: task.Tags.Select(x => x.Id).ToList());
        _taskRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);
        _taskRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ServiceException>()
            .WithMessage(Errors.Task.FailedToUpdate.Description);
    }
}