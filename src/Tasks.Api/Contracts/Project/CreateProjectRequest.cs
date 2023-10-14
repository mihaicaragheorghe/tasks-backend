namespace Tasks.Api.Contracts;

public record CreateProjectRequest(
    string Name,
    string? Color
);