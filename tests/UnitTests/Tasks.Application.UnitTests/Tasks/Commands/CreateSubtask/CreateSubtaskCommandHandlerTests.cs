using Moq;

using Tasks.Application.Common.Repository;
using Tasks.Application.Tasks.Commands;
using Tasks.Application.UnitTests.Tasks.TestUtils;

using static Tasks.Application.Common.Errors.Errors;

namespace Tasks.Application.UnitTests.Tasks.Commands;

public class CreateSubtaskCommandHandlerTests
{
    private readonly Mock<ISubtaskRepository> _subtaskRepositoryMock;
    private readonly CreateSubtaskCommandHandler _handler;

    public CreateSubtaskCommandHandlerTests()
    {
        _subtaskRepositoryMock = new Mock<ISubtaskRepository>();
        _handler = new CreateSubtaskCommandHandler(_subtaskRepositoryMock.Object);
    }
}