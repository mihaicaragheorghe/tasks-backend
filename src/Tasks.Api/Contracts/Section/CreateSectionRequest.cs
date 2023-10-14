namespace Tasks.Api.Contracts;

public record CreateSectionRequest(
    Guid ProjectId,
    string Name
);