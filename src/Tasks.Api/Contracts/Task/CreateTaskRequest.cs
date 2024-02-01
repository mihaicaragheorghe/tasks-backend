using Application.Tasks.Commands;
using Domain;

namespace Tasks.Api.Contracts;

public record CreateTaskRequest(
    Guid ProjectId,
    Guid SectionId,
    Guid AssignedToUserId,
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime? DueAtUtc,
    List<string>? SubtasksTitles,
    List<Guid>? TagsIds)
{
    public CreateTaskCommand ToCommand(Guid userId) => new(
        ProjectId,
        SectionId,
        AssignedToUserId,
        userId,
        Title,
        Description,
        Priority,
        DueAtUtc,
        TagsIds ?? new List<Guid>(),
        SubtasksTitles ?? new List<string>());
};