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

    public async Task<Project> Handle(CreateProjectCommand command, CancellationToken cancellationToken)
    {
        var project = Project.Create(command.Name, command.UserId, command.Color);

        bool success = await _taskProjectRepository.AddAsync(project, cancellationToken) > 0;

        if (!success)
        {
            throw new AppException(Errors.Project.FailedToCreate);
        }

        return project;
    }
}