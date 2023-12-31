using Microsoft.EntityFrameworkCore;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly DataContext _context;

    public ProjectRepository(DataContext context)
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

    public async Task<List<Project>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .Where(project => project.OwnerId == userId)
            .OrderBy(project => project.IsFavorite)
            .ThenBy(project => project.OrderIndex != 0)
            .ThenBy(project => project.OrderIndex)
            .ThenBy(project => project.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Projects
            .FirstOrDefaultAsync(project => project.Id == id, cancellationToken);
    } 

    public async Task<int> UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        _context.Projects.Update(project);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}
