using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Tasks;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, Project>
{
    private readonly ITaskProjectRepository _taskProjectRepository;

    public CreateProjectCommandHandler(ITaskProjectRepository taskProjectRepository)
    {
        _taskProjectRepository = taskProjectRepository;
    }

    public async Task<Project> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        // todo get current logged in user id
        var userId = new Guid("7F131BC3-9175-4B1F-B251-DEB3A96DFF0F");
        var project = Project.Create(request.Name, userId, request.Color);

        bool success = await _taskProjectRepository.AddAsync(project, cancellationToken) > 0;

        if (!success)
        {
            throw new AppException(Errors.Project.FailedToCreate);
        }

        return project;
    }
}