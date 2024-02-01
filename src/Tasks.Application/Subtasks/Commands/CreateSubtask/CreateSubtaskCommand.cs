using MediatR;
using Domain;

namespace Application.Subtasks.Commands;

public record CreateSubtaskCommand(
    Guid ParentId,
    string Title) : IRequest<Subtask>;