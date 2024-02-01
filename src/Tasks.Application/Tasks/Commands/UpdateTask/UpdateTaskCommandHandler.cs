using MediatR;

using Application.Common.ErrorHandling;

using Application.Core;

using Domain;

namespace Application.Tasks.Commands;

public class UpdateTaskCommandHandler(
    ITaskRepository taskRepository,
    IProjectRepository projectRepository,
    ISectionRepository sectionRepository,
    ITagRepository tagRepository) 
    : IRequestHandler<UpdateTaskCommand, TaskEntity>
{
    public async Task<TaskEntity> Handle(UpdateTaskCommand command, CancellationToken cancellationToken)
    {
        var task = await taskRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new ServiceException(Errors.Task.NotFound);

        if (command.ProjectId != task.ProjectId)
        {
            if (await projectRepository.GetByIdAsync(command.ProjectId, cancellationToken) is null)
                throw new ServiceException(Errors.Project.NotFound);
        }
        if (command.SectionId is not null && command.SectionId != task.SectionId)
        {
            var section = await sectionRepository.GetByIdAsync((Guid)command.SectionId, cancellationToken)
                ?? throw new ServiceException(Errors.Section.NotFound);

            if (section.ProjectId != task.ProjectId)
            {
                throw new ServiceException(Errors.Section.SectionNotInProject);
            }
        }

        var tags = await tagRepository.GetTagsByIdsAsync(command.TagsIds, cancellationToken);

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

        return await taskRepository.UpdateAsync(task, cancellationToken) > 0
            ? task
            : throw new ServiceException(Errors.Task.FailedToUpdate);
    }
}