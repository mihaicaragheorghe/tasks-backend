
using Microsoft.EntityFrameworkCore;
using Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class TaskRepository(DataContext context) : ITaskRepository
{
    public async Task<int> AddAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        await context.Tasks.AddAsync(task, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        context.Tasks.Update(task);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        context.Tasks.Remove(task);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<TaskEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .Where(t => t.AssignedToUserId == userId || t.CreatedByUserId == userId)
            .OrderByDescending(t => t.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<TaskEntity>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .Where(t => t.ProjectId == projectId)
            .OrderByDescending(t => t.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Tasks
            .AnyAsync(t => t.Id == id, cancellationToken);
    }
}