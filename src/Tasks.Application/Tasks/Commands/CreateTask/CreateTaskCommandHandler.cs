using MediatR;
using Tasks.Application.Common.ErrorHandling;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskEntity>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, ITagRepository tagRepository)
    {
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
    }

    public async Task<TaskEntity> Handle(CreateTaskCommand command, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetTagsByIdsAsync(command.TagsIds, cancellationToken);

        var task = TaskEntity.Create(
            command.CreatedByUserId,
            command.AssignedToUserId,
            command.ProjectId,
            command.SectionId,
            command.Title,
            command.Description,
            command.Priority,
            command.DueAtUtc,
            tags.ToList());

        command.SubtasksTitles
            .Select(x => Subtask.Create(task.Id, x))
            .ToList()
            .ForEach(x => task.Subtasks.Add(x));

        bool success = await _taskRepository.AddAsync(task, cancellationToken) > 0;

        if (!success)
        {
            throw new ServiceException(Errors.Task.FailedToCreate);
        }

        return task;
    }
}