using MediatR;
using Domain;

namespace Application.Comments.Queries
{
    public class GetCommentsByTaskId(Guid taskId) : IRequest<IList<Comment>>
    {
        public Guid TaskId { get; } = taskId;
    }

    public class GetCommentsByTaskIdHandler(ICommentRepository commentRepository) 
        : IRequestHandler<GetCommentsByTaskId, IList<Comment>>
    {
        private readonly ICommentRepository _commentRepository = commentRepository;

        public async Task<IList<Comment>> Handle(GetCommentsByTaskId request, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetByTaskIdAsync(request.TaskId, cancellationToken);
        }
    }
}