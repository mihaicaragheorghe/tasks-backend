using FluentAssertions;

using MediatR;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.Projects.Commands;
using Application.UnitTests.TestUtils.Projects;
using Domain;

namespace Application.UnitTests.Projects.Commands;

public class DeleteProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly DeleteProjectCommandHandler _deleteProjectCommandHandler;

    public DeleteProjectCommandHandlerTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _deleteProjectCommandHandler = new DeleteProjectCommandHandler(_projectRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldDeleteProject()
    {
        // Arrange
        var project = ProjectUtils.CreateProject();
        var command = new DeleteProjectCommand(project.Id);
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(project.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);
        _projectRepositoryMock
            .Setup(x => x.DeleteAsync(project, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _deleteProjectCommandHandler.Handle(command, default);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task Handle_NonExistingProject_ShouldThrowValidationException()
    {
        // Arrange
        var command = new DeleteProjectCommand(Guid.NewGuid());
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Project?)null);

        // Act
        Func<Task> act = async () => await _deleteProjectCommandHandler.Handle(command, default);

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
        var command = new DeleteProjectCommand(project.Id);
        _projectRepositoryMock
            .Setup(x => x.GetByIdAsync(project.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(project);
        _projectRepositoryMock
            .Setup(x => x.DeleteAsync(project, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _deleteProjectCommandHandler.Handle(command, default);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Project.DeleteFailed.Description);
    }
}