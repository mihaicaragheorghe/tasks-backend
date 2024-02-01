using MediatR;

using Application.Common.ErrorHandling;

using Application.Core;
using Domain;

namespace Application.Projects.Commands;

public record DeleteProjectCommand(Guid Id) : IRequest<Unit>;

public class DeleteProjectCommandHandler(IProjectRepository projectRepository) 
    : IRequestHandler<DeleteProjectCommand, Unit>
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<Unit> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken) 
            ?? throw new ServiceException(Errors.Project.NotFound);

        return await _projectRepository.DeleteAsync(project, cancellationToken) > 0
            ? Unit.Value
            : throw new ServiceException(Errors.Project.DeleteFailed);
    }
}