namespace Tasks.Api.Contracts;

public record CreateSubtaskRequest(Guid ParentId, string Title);