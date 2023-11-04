using MediatR;

using Tasks.Application.Common.Errors;

using Tasks.Application.Common.Repository;
using Tasks.Application.Core;

namespace Tasks.Application.Tasks.Commands;

public record DeleteSubtaskCommand(Guid Id) : IRequest<Unit>;

public class DeleteSubtaskCommandHandler : IRequestHandler<DeleteSubtaskCommand, Unit>
{
    private readonly ISubtaskRepository _subtaskRepository;

    public DeleteSubtaskCommandHandler(ISubtaskRepository subtaskRepository)
    {
        _subtaskRepository = subtaskRepository;
    }

    public async Task<Unit> Handle(DeleteSubtaskCommand request, CancellationToken cancellationToken)
    {
        var subtask = await _subtaskRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Subtask.NotFound);

        await _subtaskRepository.DeleteAsync(subtask, cancellationToken);
        return Unit.Value;
    }
}