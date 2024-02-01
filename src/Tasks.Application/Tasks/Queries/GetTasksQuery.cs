using MediatR;
using Domain;

namespace Application.Tasks.Queries;

public record GetTasksQuery(Guid UserId) : IRequest<List<TaskEntity>>;

public class GetTasksQueryHandler(ITaskRepository taskRepository) 
    : IRequestHandler<GetTasksQuery, List<TaskEntity>>
{
    public async Task<List<TaskEntity>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        return await taskRepository.GetAllAsync(request.UserId, cancellationToken);
    }
}