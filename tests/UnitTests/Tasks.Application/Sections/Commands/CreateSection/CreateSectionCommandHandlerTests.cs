using FluentAssertions;

using Moq;

using Application.Common.ErrorHandling;
using Application.Core;
using Application.Sections.Commands;
using Application.UnitTests.TestUtils.Sections;
using Domain;

namespace Application.UnitTests.Sections.Commands;

public class CreateSectionCommandHandlerTests
{
    private readonly Mock<ISectionRepository> _sectionRepositoryMock;
    private readonly CreateSectionCommandHandler _handler;

    public CreateSectionCommandHandlerTests()
    {
        _sectionRepositoryMock = new Mock<ISectionRepository>();
        _handler = new CreateSectionCommandHandler(_sectionRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnSection_WhenSectionCreated()
    {
        // Arrange
        var command = CreateSectionCommandUtils.CreateCommand();
        _sectionRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Section>(), It.IsAny<CancellationToken>()))
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
        var command = CreateSectionCommandUtils.CreateCommand();
        _sectionRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Section>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(0);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<ServiceException>()
            .WithMessage(Errors.Section.FailedToCreate.Description);
    }
}