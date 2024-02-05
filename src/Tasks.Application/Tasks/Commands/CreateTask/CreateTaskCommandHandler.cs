using MediatR;
using Application.Common.ErrorHandling;
using Application.Core;
using Domain;

namespace Application.Tasks.Commands;

public class CreateTaskCommandHandler(ITaskRepository taskRepository, ITagRepository tagRepository) 
    : IRequestHandler<CreateTaskCommand, TaskEntity>
{
    public async Task<TaskEntity> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        // todo - validate user ids, project id, section id, tags ids
        var tags = await tagRepository.GetTagsByIdsAsync(command.TagsIds, cancellationToken);

        var task = TaskEntity.Create(
            command.CreatedByUserId,
            command.AssignedToUserId,
            command.ProjectId,
            command.SectionId,
            command.Title,
            command.Description,
            command.Priority,
            command.DueAtUtc,
            [.. tags]);

        command.SubtasksTitles
            .Select(x => Subtask.Create(task.Id, x))
            .ToList()
            .ForEach(x => task.Subtasks.Add(x));

        bool success = await taskRepository.AddAsync(task, cancellationToken) > 0;

        return !success ? throw new ServiceException(Errors.Task.FailedToCreate) : task;
    }
}