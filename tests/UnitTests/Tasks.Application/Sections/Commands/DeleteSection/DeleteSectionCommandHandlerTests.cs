using FluentAssertions;

using MediatR;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.Sections.Commands;
using Application.UnitTests.TestUtils.Sections;
using Domain;

namespace Application.UnitTests.Sections.Commands;

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