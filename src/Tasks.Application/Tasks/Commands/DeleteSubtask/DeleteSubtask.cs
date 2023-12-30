using MediatR;

using Tasks.Application.Common.ErrorHandling;

using Tasks.Application.Common.Repository;
using Tasks.Application.Core;

namespace Tasks.Application.Tasks.Commands;

public record DeleteSubtaskCommand(Guid Id) : IRequest<Unit>;

public class DeleteSubtaskCommandHandler(ISubtaskRepository subtaskRepository) 
    : IRequestHandler<DeleteSubtaskCommand, Unit>
{
    private readonly ISubtaskRepository _subtaskRepository = subtaskRepository;

    public async Task<Unit> Handle(DeleteSubtaskCommand request, CancellationToken cancellationToken)
    {
        var subtask = await _subtaskRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Subtask.NotFound);

        bool success = await _subtaskRepository.DeleteAsync(subtask, cancellationToken) > 0;

        return !success ? throw new ServiceException(Errors.Subtask.FailedToDelete) : Unit.Value;
    }
}