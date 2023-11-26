using FluentAssertions;

using MediatR;

using Moq;

using Tasks.Application.Common.Errors;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Application.TaskSections.Commands;
using Tasks.Application.UnitTests.TestUtils.Sections;
using Tasks.Domain;

namespace Tasks.Application.UnitTests.TaskSections.Commands;

public class DeleteSectionCommandHandlerTests
{
    private readonly Mock<ISectionRepository> _sectionRepositoryMock;
    private readonly DeleteSectionCommandHandler _handler;

    public DeleteSectionCommandHandlerTests()
    {
        _sectionRepositoryMock = new Mock<ISectionRepository>();
        _handler = new DeleteSectionCommandHandler(_sectionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSection_WhenSectionDeleted()
    {
        // Arrange
        var section = SectionUtils.CreateSection();
        var command = new DeleteSectionCommand(section.Id);
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(section);
        _sectionRepositoryMock
            .Setup(x => x.DeleteAsync(section, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
        _sectionRepositoryMock.Verify(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()), Times.Once);
        _sectionRepositoryMock.Verify(x => x.DeleteAsync(section, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenSectionNotFound()
    {
        // Arrange
        var command = new DeleteSectionCommand(Guid.NewGuid());
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Section?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Section.NotFound.Description);
        _sectionRepositoryMock.Verify(x => x.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()), Times.Once);
        _sectionRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Section>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRepositoryFails()
    {
        // Arrange
        var section = SectionUtils.CreateSection();
        var command = new DeleteSectionCommand(section.Id);
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(section);
        _sectionRepositoryMock
            .Setup(x => x.DeleteAsync(section, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Section.DeleteFailed.Description);
    }
}