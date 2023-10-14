using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetTaskByIdQuery(Guid TaskId) : IRequest<TaskEntity?>;

public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskEntity?>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskByIdQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskEntity?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetByIdAsync(request.TaskId, cancellationToken);
    }
}