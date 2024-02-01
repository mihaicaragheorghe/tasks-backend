using MediatR;
using Domain;

namespace Application.Tasks.Commands;

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