using MediatR;
using Application.Common.ErrorHandling;
using Application.Core;
using Domain;

namespace Application.Projects.Commands;

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