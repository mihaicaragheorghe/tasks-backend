using MediatR;

using Application.Common.ErrorHandling;
using Application.Core;

using Domain;

namespace Application.Subtasks.Commands
{
    public class UpdateSubtaskCommandHandler(ISubtaskRepository subtaskRepository) 
        : IRequestHandler<UpdateSubtaskCommand, Subtask>
    {
        private readonly ISubtaskRepository _subtaskRepository = subtaskRepository;

        public async Task<Subtask> Handle(UpdateSubtaskCommand command, CancellationToken cancellationToken)
        {
            var subtask = await _subtaskRepository.GetByIdAsync(command.Id, cancellationToken)
                ?? throw new ServiceException(Errors.Subtask.NotFound);

            subtask.Update(command.Title, command.IsCompleted);

            return await _subtaskRepository.UpdateAsync(subtask, cancellationToken) > 0
                ? subtask
                : throw new ServiceException(Errors.Subtask.FailedToUpdate);
        }
    }
}