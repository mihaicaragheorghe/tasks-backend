using MediatR;
using Domain;

namespace Application.Tasks.Queries;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<TaskEntity?>;

public class GetTaskByIdQueryHandler(ITaskRepository taskRepository) : IRequestHandler<GetTaskByIdQuery, TaskEntity?>
{
    public async Task<TaskEntity?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
    }
}