using MediatR;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Tasks;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, TaskProject>
{
    private readonly ITaskProjectRepository _taskProjectRepository;

    public CreateProjectCommandHandler(ITaskProjectRepository taskProjectRepository)
    {
        _taskProjectRepository = taskProjectRepository;
    }

    public async Task<TaskProject> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = TaskProject.Create(request.Name, request.Color);

        bool success = await _taskProjectRepository.AddAsync(project, cancellationToken) > 0;

        if (!success)
        {
            throw new AppException(Errors.Task.FailedToCreate);
        }

        return project;
    }
}