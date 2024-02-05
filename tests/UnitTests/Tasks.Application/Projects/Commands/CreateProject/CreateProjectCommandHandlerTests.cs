using FluentAssertions;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.Projects.Commands;
using Application.UnitTests.TestUtils.Projects;
using Domain;

namespace Application.UnitTests.Projects.Commands;

public class CreateProjectCommandHandlerTests
{
    private readonly Mock<IProjectRepository> _mockRepository;
    private readonly CreateProjectCommandHandler _handler;

    public CreateProjectCommandHandlerTests()
    {
        _mockRepository = new Mock<IProjectRepository>();
        _handler = new CreateProjectCommandHandler(_mockRepository.Object);
    }

    [Theory]
    [MemberData(nameof(ValidCreateProjectCommands))]
    public async Task Handle_ValidCommand_ShouldCreateProject(CreateProjectCommand command)
    {
        // Arrange
        _mockRepository
            .Setup(x => x.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var project = await _handler.Handle(command, default);

        // Assert
        project.ValidateCreatedFrom(command);
    }

    [Fact]
    public async Task Handle_RepositoryFails_ShouldThrowServiceException()
    {
        // Arrange
        _mockRepository
            .Setup(x => x.AddAsync(It.IsAny<Project>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(CreateProjectCommandUtils.CreateProject(), default);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Project.FailedToCreate.Description);
    }

    public static IEnumerable<object[]> ValidCreateProjectCommands()
    {
        yield return new object[] { CreateProjectCommandUtils.CreateProject() };
    }
}