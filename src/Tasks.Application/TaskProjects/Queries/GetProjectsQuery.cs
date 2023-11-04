using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Queries;

public record GetProjectsQuery(Guid UserId) : IRequest<List<Project>>;

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery, List<Project>>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectsQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<Project>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        return await _projectRepository.GetAllAsync(request.UserId, cancellationToken);
    }
}