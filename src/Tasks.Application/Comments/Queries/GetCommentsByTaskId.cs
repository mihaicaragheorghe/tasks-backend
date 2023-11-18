using MediatR;

using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Application.Comments.Queries
{
    public class GetCommentsByTaskId : IRequest<IList<Comment>>
    {
        public Guid TaskId { get; }

        public GetCommentsByTaskId(Guid taskId)
        {
            TaskId = taskId;
        }
    }

    public class GetCommentsByTaskIdHandler : IRequestHandler<GetCommentsByTaskId, IList<Comment>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetCommentsByTaskIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IList<Comment>> Handle(GetCommentsByTaskId request, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetByTaskIdAsync(request.TaskId, cancellationToken);
        }
    }
}