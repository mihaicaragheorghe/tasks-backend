using FluentAssertions;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.Sections.Commands;
using Application.UnitTests.TestUtils.Sections;
using Domain;

namespace Application.UnitTests.Sections.Commands;

public class UpdateSectionCommandHandlerTests
{
    private readonly Mock<ISectionRepository> _sectionRepositoryMock;
    private readonly UpdateSectionCommandHandler _handler;

    public UpdateSectionCommandHandlerTests()
    {
        _sectionRepositoryMock = new Mock<ISectionRepository>();
        _handler = new UpdateSectionCommandHandler(_sectionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSection_WhenSectionUpdated()
    {
        // Arrange
        var section = SectionUtils.CreateSection();
        var command = UpdateSectionCommandUtils.CreateCommand(section.Id);
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(section);
        _sectionRepositoryMock
            .Setup(x => x.UpdateAsync(section, It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.ValidateUpdatedFrom(result, command);
        _sectionRepositoryMock.Verify(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()), Times.Once);
        _sectionRepositoryMock.Verify(x => x.UpdateAsync(result, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenSectionNotFound()
    {
        // Arrange
        var command = UpdateSectionCommandUtils.CreateCommand(Guid.NewGuid());
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
        _sectionRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Section>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRepositoryFails()
    {
        // Arrange
        var section = SectionUtils.CreateSection();
        var command = UpdateSectionCommandUtils.CreateCommand(section.Id);
        _sectionRepositoryMock
            .Setup(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(section);
        _sectionRepositoryMock
            .Setup(x => x.UpdateAsync(section, It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Section.FailedToUpdate.Description);
        _sectionRepositoryMock.Verify(x => x.GetByIdAsync(section.Id, It.IsAny<CancellationToken>()), Times.Once);
        _sectionRepositoryMock.Verify(x => x.UpdateAsync(section, It.IsAny<CancellationToken>()), Times.Once);
    }
}