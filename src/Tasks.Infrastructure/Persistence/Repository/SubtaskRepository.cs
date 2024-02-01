using Microsoft.EntityFrameworkCore;
using Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class SubtaskRepository(DataContext context) : ISubtaskRepository
{
    public async Task<int> AddAsync(Subtask subtask, CancellationToken cancellationToken = default)
    {
        await context.Subtasks.AddAsync(subtask, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Subtask subtask, CancellationToken cancellationToken = default)
    {
        context.Subtasks.Update(subtask);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Subtask subtask, CancellationToken cancellationToken = default)
    {
        context.Subtasks.Remove(subtask);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Subtask>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default)
    {
        return await context.Subtasks
            .Where(s => s.ParentId == taskId)
            .OrderBy(s => s.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Subtask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Subtasks.FindAsync(id);
    }
}