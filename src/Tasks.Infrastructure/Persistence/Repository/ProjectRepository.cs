using Microsoft.EntityFrameworkCore;
using Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class ProjectRepository(DataContext context) : IProjectRepository
{
    public async Task<int> AddAsync(Project project, CancellationToken cancellationToken = default)
    {
        await context.Projects.AddAsync(project, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Project project, CancellationToken cancellationToken = default)
    {
        context.Projects.Remove(project);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Project>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .Where(project => project.OwnerId == userId)
            .OrderBy(project => project.IsFavorite)
            .ThenBy(project => project.OrderIndex != 0)
            .ThenBy(project => project.OrderIndex)
            .ThenBy(project => project.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Projects
            .FirstOrDefaultAsync(project => project.Id == id, cancellationToken);
    } 

    public async Task<int> UpdateAsync(Project project, CancellationToken cancellationToken = default)
    {
        context.Projects.Update(project);
        return await context.SaveChangesAsync(cancellationToken);
    }
}
