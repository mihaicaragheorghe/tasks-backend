using MediatR;
using Domain;

namespace Application.Tasks.Queries;

public record GetSubtasksQuery(Guid TaskId) : IRequest<List<Subtask>>;

public class GetSubtasksQueryHandler(ISubtaskRepository subtaskRepository) 
    : IRequestHandler<GetSubtasksQuery, List<Subtask>>
{
    public async Task<List<Subtask>> Handle(GetSubtasksQuery request, CancellationToken cancellationToken)
    {
        return await subtaskRepository.GetByTaskIdAsync(request.TaskId, cancellationToken);
    }
}