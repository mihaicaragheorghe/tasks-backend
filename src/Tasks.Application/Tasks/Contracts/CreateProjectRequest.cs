namespace Tasks.Application.Tasks;

public record CreateProjectRequest(
    string Name,
    string? Color
);