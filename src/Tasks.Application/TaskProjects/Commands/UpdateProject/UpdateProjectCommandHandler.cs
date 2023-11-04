using MediatR;

using Tasks.Application.Common.Errors;

using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.TaskProjects.Commands;

public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, Project>
{
    private readonly IProjectRepository _projectRepository;

    public UpdateProjectCommandHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<Project> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Project.NotFound);

        project.Update(
            request.Name,
            request.Color,
            request.Order,
            request.IsArchived,
            request.IsFavorite);

        bool succcess = await _projectRepository.UpdateAsync(project, cancellationToken) > 0;

        if (!succcess)
        {
            throw new ServiceException(Errors.Project.FailedToUpdate);
        }

        return project;
    }
}