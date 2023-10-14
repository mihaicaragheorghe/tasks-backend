
using Microsoft.EntityFrameworkCore;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class TaskRepository : ITaskRepository
{
    private readonly DataContext _context;

    public TaskRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        await _context.Tasks.AddAsync(task, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default)
    {
        _context.Tasks.Remove(task);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments)
            .Include(t => t.Tags)
            .Include(t => t.Subtasks)
            .Where(t => t.AssignedToUserId == userId || t.CreatedByUserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments)
            .Include(t => t.Tags)
            .Include(t => t.Subtasks)
            .Where(t => t.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .Include(t => t.CreatedBy)
            .Include(t => t.AssignedTo)
            .Include(t => t.Comments)
            .Include(t => t.Tags)
            .Include(t => t.Subtasks)
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }
}