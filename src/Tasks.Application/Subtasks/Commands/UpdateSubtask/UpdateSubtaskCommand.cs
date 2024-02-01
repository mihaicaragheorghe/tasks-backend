using MediatR;

using Domain;

namespace Application.Subtasks.Commands;

public record UpdateSubtaskCommand(
    Guid Id,
    string Title,
    bool IsCompleted) : IRequest<Subtask>;