using MediatR;
using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record CreateSubtaskCommand(
    Guid ParentId,
    string Title) : IRequest<Subtask>;