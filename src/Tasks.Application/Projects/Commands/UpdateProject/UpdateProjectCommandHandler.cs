using MediatR;

using Application.Common.ErrorHandling;

using Application.Core;
using Domain;

namespace Application.Projects.Commands;

public class UpdateProjectCommandHandler(IProjectRepository projectRepository) 
    : IRequestHandler<UpdateProjectCommand, Project>
{
    private readonly IProjectRepository _projectRepository = projectRepository;

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

        return !succcess ? throw new ServiceException(Errors.Project.FailedToUpdate) : project;
    }
}