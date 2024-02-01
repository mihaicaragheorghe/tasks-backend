using Microsoft.EntityFrameworkCore;
using Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class CommentRepository(DataContext context) : ICommentRepository
{
    public async Task<int> AddAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await context.Comments.AddAsync(comment, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        context.Comments.Update(comment);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        context.Comments.Remove(comment);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Comment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await context.Comments
            .Where(c => c.TaskId == taskId)
            .OrderByDescending(c => c.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Comments.FindAsync(id);
    }
}