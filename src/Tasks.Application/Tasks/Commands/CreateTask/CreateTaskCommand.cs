using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record CreateTaskCommand(
    Guid ProjectId,
    Guid SectionId,
    Guid AssignedToUserId,
    Guid CreatedByUserId,
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? DueAtUtc,
    List<Guid> TagsIds,
    List<string> SubtasksTitles
) : IRequest<TaskEntity>;