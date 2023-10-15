using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetTasksQuery(Guid UserId) : IRequest<List<TaskEntity>>;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskEntity>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskEntity>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetAllAsync(request.UserId, cancellationToken);
    }
}