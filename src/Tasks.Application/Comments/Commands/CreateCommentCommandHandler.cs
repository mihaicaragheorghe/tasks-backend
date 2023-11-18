using MediatR;

using Tasks.Application.Common.Errors;
using Tasks.Application.Common.Repository;
using Tasks.Application.Core;
using Tasks.Domain;

namespace Tasks.Application.Comments.Commands;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Comment>
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITaskRepository _taskRepository;

    public CreateCommentCommandHandler(
        ICommentRepository commentRepository,
        ITaskRepository taskRepository)
    {
        _commentRepository = commentRepository;
        _taskRepository = taskRepository;
    }

    public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comment = Comment.Create(request.TaskId, request.UserId, request.Content);
        bool success = await _commentRepository.AddAsync(comment, cancellationToken) > 0;

        return success ? comment
            : throw new ServiceException(Errors.Comments.FailedToCreate);
    }
}