using Microsoft.EntityFrameworkCore;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly DataContext _context;

    public CommentRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await _context.Comments.AddAsync(comment, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        _context.Comments.Update(comment);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        _context.Comments.Remove(comment);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Comment>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await _context.Comments
            .Where(c => c.TaskId == taskId)
            .OrderByDescending(c => c.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Comment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Comments.FindAsync(id);
    }
}