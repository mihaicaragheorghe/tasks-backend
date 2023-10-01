using Microsoft.EntityFrameworkCore;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class TaskProjectRepository : ITaskProjectRepository
{
    private readonly DataContext _context;

    public TaskProjectRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Project project, CancellationToken cancellationToken = default)
    {
        _context.Projects.Remove(project);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Projects.ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<int> UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        _context.Projects.Update(project);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
