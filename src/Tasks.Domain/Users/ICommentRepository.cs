namespace Domain;

public interface ICommentRepository
{
    public Task<List<Comment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    public Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<int> AddAsync(Comment comment, CancellationToken cancellationToken = default);
    public Task<int> UpdateAsync(Comment comment, CancellationToken cancellationToken = default);
    public Task<int> DeleteAsync(Comment comment, CancellationToken cancellationToken = default);
}