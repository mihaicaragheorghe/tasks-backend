using MediatR;

using Application.Common.ErrorHandling;
using Application.Core;
using Domain;

namespace Application.Comments.Commands;

public class CreateCommentCommandHandler(
    ICommentRepository commentRepository,
    ITaskRepository taskRepository) : IRequestHandler<CreateCommentCommand, Comment>
{
    private readonly ICommentRepository _commentRepository = commentRepository;
    private readonly ITaskRepository _taskRepository = taskRepository;

    public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        if (!await _taskRepository.ExistsAsync(request.TaskId, cancellationToken))
            throw new ServiceException(Errors.Task.NotFound);

        var comment = Comment.Create(request.TaskId, request.UserId, request.Content);
        bool success = await _commentRepository.AddAsync(comment, cancellationToken) > 0;

        return success ? comment
            : throw new ServiceException(Errors.Comments.FailedToCreate);
    }
}