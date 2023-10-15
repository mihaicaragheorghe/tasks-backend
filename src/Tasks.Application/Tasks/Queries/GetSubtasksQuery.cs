using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Queries;

public record GetSubtasksQuery(Guid TaskId) : IRequest<List<Subtask>>;

public class GetSubtasksQueryHandler : IRequestHandler<GetSubtasksQuery, List<Subtask>>
{
    private readonly ISubtaskRepository _subtaskRepository;

    public GetSubtasksQueryHandler(ISubtaskRepository subtaskRepository)
    {
        _subtaskRepository = subtaskRepository;
    }

    public async Task<List<Subtask>> Handle(GetSubtasksQuery request, CancellationToken cancellationToken)
    {
        return await _subtaskRepository.GetByTaskIdAsync(request.TaskId, cancellationToken);
    }
}