using Tasks.Domain;

namespace Tasks.Api.Contracts;

public record ProjectDto(
    Guid Id,
    string Name,
    string? Color,
    bool IsArchived,
    bool IsFavorite,
    ICollection<SectionDto> Sections,
    ICollection<TaskDto> Tasks,
    ICollection<UserProfileDto> Collaborators)
{
    public ProjectDto(Project project)
        : this(
            project.Id,
            project.Name,
            project.Color,
            project.IsArchived,
            project.IsFavorite,
            project.Sections
                .OrderBy(x => x.Order != 0)
                .ThenBy(x => x.Order)
                .Select(x => new SectionDto(x))
                .ToList(),
            project.Tasks
                .OrderBy(x => x.CreatedAtUtc)
                .Select(x => new TaskDto(x))
                .ToList(),
            project.Collaborators
                .OrderBy(x => x.DisplayName)
                .Select(x => new UserProfileDto(x))
                .ToList()) { }
}