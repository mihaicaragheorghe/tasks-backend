using Domain;

namespace Tasks.Api.Contracts;

public record SectionDto(
    Guid Id,
    Guid ProjectId,
    string Name,
    ICollection<TaskDto> Tasks)
{
    public SectionDto(Section section)
        : this(
            section.Id,
            section.ProjectId,
            section.Name,
            section.Tasks
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new TaskDto(x))
                .ToList()) { }
}