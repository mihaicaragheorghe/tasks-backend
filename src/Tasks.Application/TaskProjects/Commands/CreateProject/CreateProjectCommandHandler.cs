using MediatR;
using Tasks.Application.Common.ErrorHandling;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Commands;

public class CreateProjectCommandHandler(IProjectRepository taskProjectRepository) 
    : IRequestHandler<CreateProjectCommand, Project>
{
    private readonly IProjectRepository _taskProjectRepository = taskProjectRepository;

    public async Task<Project> Handle(
        CreateProjectCommand command, 
        CancellationToken cancellationToken)
    {
        var project = Project.Create(command.Name, command.UserId, command.Color);

        bool success = await _taskProjectRepository.AddAsync(project, cancellationToken) > 0;

        return !success ? throw new ServiceException(Errors.Project.FailedToCreate) : project;
    }
}