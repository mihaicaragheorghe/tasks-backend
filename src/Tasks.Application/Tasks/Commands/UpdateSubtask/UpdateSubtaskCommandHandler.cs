using MediatR;

using Tasks.Application.Common.Errors;

using Tasks.Application.Common.Repository;
using Tasks.Application.Core;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands
{
    public class UpdateSubtaskCommandHandler : IRequestHandler<UpdateSubtaskCommand, Subtask>
    {
        private readonly ISubtaskRepository _subtaskRepository;

        public UpdateSubtaskCommandHandler(ISubtaskRepository subtaskRepository)
        {
            _subtaskRepository = subtaskRepository;
        }

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