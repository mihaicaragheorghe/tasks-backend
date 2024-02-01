using MediatR;
using Domain;

namespace Application.Projects.Queries;

public record GetProjectsQuery(Guid UserId) : IRequest<List<Project>>;

public class GetProjectsQueryHandler(IProjectRepository projectRepository) 
    : IRequestHandler<GetProjectsQuery, List<Project>>
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<List<Project>> Handle(
        GetProjectsQuery request, 
        CancellationToken cancellationToken = default)
    {
        return await _projectRepository.GetAllAsync(request.UserId, cancellationToken);
    }
}