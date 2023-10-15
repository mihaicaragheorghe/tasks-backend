namespace Tasks.Api.Contracts;

public record UpdateProjectRequest(
    string Name,
    string? Color,
    int Order,
    bool IsArchived,
    bool IsFavorite);