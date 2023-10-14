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
            project.Sections.Select(x => new SectionDto(x)).ToList(),
            project.Tasks.Select(x => new TaskDto(x)).ToList(),
            project.Collaborators.Select(x => new UserProfileDto(x)).ToList()) { }
}