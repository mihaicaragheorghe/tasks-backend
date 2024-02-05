using FluentAssertions;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.Projects.Commands;
using Application.UnitTests.TestUtils.Projects;
using Domain;

namespace Application.UnitTests.Projects.Commands;

public class UpdateProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly UpdateProjectCommandHandler _updateProjectCommandHandler;

    public UpdateProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _updateProjectCommandHandler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldUpdateProject()
    {
        // Arrange
        var project = ProjectUtils.CreateProject();
        var command = UpdateProjectCommandUtils.CreateCommand(project.Id);
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(project.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);
        _projectRepositoryMock
            .Setup(x => x.UpdateAsync(project, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var updatedProject = await _updateProjectCommandHandler.Handle(command, default);

        // Assert
        updatedProject.ValidateUpdatedFrom(project, command);
    }

    [Fact]
    public async Task Handle_NonExistingProject_ShouldThrowValidationException()
    {
        // Arrange
        var command = UpdateProjectCommandUtils.CreateCommand(Guid.NewGuid());
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project?)null);

        // Act
        Func<Task> act = async () => await _updateProjectCommandHandler.Handle(command, default);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Project.NotFound.Description);
    }

    [Fact]
    public async Task Handle_RepositoryFails_ShouldThrowServiceException()
    {
        // Arrange
        var project = ProjectUtils.CreateProject();
        var command = UpdateProjectCommandUtils.CreateCommand(project.Id);
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(project.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);
        _projectRepositoryMock
            .Setup(x => x.UpdateAsync(project, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _updateProjectCommandHandler.Handle(command, default);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Project.FailedToUpdate.Description);
    }
}