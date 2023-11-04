using MediatR;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public record UpdateSubtaskCommand(
    Guid Id,
    string Title,
    bool IsCompleted) : IRequest<Subtask>;