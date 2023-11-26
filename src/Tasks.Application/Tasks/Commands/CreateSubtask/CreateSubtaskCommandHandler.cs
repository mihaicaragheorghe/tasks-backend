using MediatR;
using Tasks.Application.Common.Errors;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class CreateSubtaskCommandHandler(ISubtaskRepository subtaskRepository) 
    : IRequestHandler<CreateSubtaskCommand, Subtask>
{
    private readonly ISubtaskRepository _subtaskRepository = subtaskRepository;

    public async Task<Subtask> Handle(CreateSubtaskCommand command, CancellationToken cancellationToken)
    {
        var subtask = Subtask.Create(command.ParentId, command.Title);

        bool success = await _subtaskRepository.AddAsync(subtask, cancellationToken) > 0;

        return !success ? throw new ServiceException(Errors.Subtask.FailedToCreate) : subtask;
    }
}