using MediatR;

using Tasks.Application.Common.Errors;
using Tasks.Application.Common.Repository;

using Tasks.Application.Core;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, TaskEntity>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ISectionRepository _sectionRepository;
    private readonly ITagRepository _tagRepository;

    public UpdateTaskCommandHandler(
        ITaskRepository taskRepository,
        IProjectRepository projectRepository,
        ISectionRepository sectionRepository,
        ITagRepository tagRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _sectionRepository = sectionRepository;
        _tagRepository = tagRepository;
    }

    public async Task<TaskEntity> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Task.NotFound);

        if (command.ProjectId != task.ProjectId)
        {
            if (await _projectRepository.GetByIdAsync(command.ProjectId, cancellationToken) is null)
                throw new ServiceException(Errors.Project.NotFound);
        }
        if (command.SectionId is not null && command.SectionId != task.SectionId)
        {
            var section = await _sectionRepository.GetByIdAsync((Guid)command.SectionId, cancellationToken)
                ?? throw new ServiceException(Errors.Section.NotFound);

            if (section.ProjectId != task.ProjectId)
            {
                throw new ServiceException(Errors.Section.SectionNotInProject);
            }
        }

        var tags = await _tagRepository.GetTagsByIdsAsync(command.TagsIds, cancellationToken);

        task.Update(
            command.Title,
            command.Description,
            command.Priority,
            command.IsComplete,
            command.DueAtUtc,
            command.IsDeleted,
            command.OrderIndex,
            command.ProjectId,
            command.SectionId,
            command.AssignedToUserId,
            tags);

        return await _taskRepository.UpdateAsync(task, cancellationToken) > 0
            ? task
            : throw new ServiceException(Errors.Task.FailedToUpdate);
    }
}