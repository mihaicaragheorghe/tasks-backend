using Microsoft.EntityFrameworkCore;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class SubtaskRepository : ISubtaskRepository
{
    private readonly DataContext _context;

    public SubtaskRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Subtask subtask, CancellationToken cancellationToken = default)
    {
        await _context.Subtasks.AddAsync(subtask, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Subtask subtask, CancellationToken cancellationToken = default)
    {
        _context.Subtasks.Update(subtask);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Subtask subtask, CancellationToken cancellationToken = default)
    {
        _context.Subtasks.Remove(subtask);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Subtask>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await _context.Subtasks
            .Where(s => s.ParentId == taskId)
            .OrderBy(s => s.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Subtask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Subtasks.FindAsync(id);
    }
}