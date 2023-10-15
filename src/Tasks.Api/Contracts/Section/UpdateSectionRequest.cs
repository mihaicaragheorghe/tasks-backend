namespace Tasks.Api.Contracts;

public record UpdateSectionRequest(
    string Name,
    int OrderIndex);