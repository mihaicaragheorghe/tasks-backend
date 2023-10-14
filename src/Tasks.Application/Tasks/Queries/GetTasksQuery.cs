using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetTasksQuery(Guid UserId) : IRequest<IEnumerable<TaskEntity>>;

public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, IEnumerable<TaskEntity>>
{
    private readonly ITaskRepository _taskRepository;

    public GetTasksQueryHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskEntity>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await _taskRepository.GetAllAsync(request.UserId, cancellationToken);
    }
}