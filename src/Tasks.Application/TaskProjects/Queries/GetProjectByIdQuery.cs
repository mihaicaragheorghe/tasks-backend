using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Queries;

public record GetProjectByIdQuery(Guid ProjectId) : IRequest<Project?>;

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, Project?>
{
    private readonly IProjectRepository _projectRepository;

    public GetProjectByIdQueryHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project?> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        return await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
    }
}