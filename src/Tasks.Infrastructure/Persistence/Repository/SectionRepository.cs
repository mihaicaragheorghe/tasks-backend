using Microsoft.EntityFrameworkCore;
using Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class SectionRepository(DataContext context) : ISectionRepository
{
    public async Task<int> AddAsync(Section section, CancellationToken cancellationToken = default)
    {
        await context.Sections.AddAsync(section, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Section section, CancellationToken cancellationToken = default)
    {
        context.Sections.Remove(section);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Section>> GetAllAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await context.Sections
            .Where(section => section.ProjectId == projectId)
            .OrderBy(section => section.OrderIndex != 0)
            .ThenBy(section => section.OrderIndex)
            .ThenBy(section => section.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Sections
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task<int> UpdateAsync(Section section, CancellationToken cancellationToken = default)
    {
        context.Sections.Update(section);
        return await context.SaveChangesAsync(cancellationToken);
    }
}