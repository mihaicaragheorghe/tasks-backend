using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Queries;

public record GetProjectByIdQuery(Guid ProjectId) : IRequest<Project?>;

public class GetProjectByIdQueryHandler(IProjectRepository projectRepository) 
    : IRequestHandler<GetProjectByIdQuery, Project?>
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Project?> Handle(
        GetProjectByIdQuery request, 
        CancellationToken cancellationToken = default)
    {
        return await _projectRepository.GetByIdAsync(request.ProjectId, cancellationToken);
    }
}