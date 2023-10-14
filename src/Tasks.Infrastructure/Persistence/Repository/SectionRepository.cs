using Microsoft.EntityFrameworkCore;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class SectionRepository : ISectionRepository
{
    private readonly DataContext _context;

    public SectionRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Section section, CancellationToken cancellationToken = default)
    {
        await _context.Sections.AddAsync(section);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Section section, CancellationToken cancellationToken = default)
    {
        _context.Sections.Remove(section);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Section>> GetAllAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return await _context.Sections
            .Where(s => s.ProjectId == projectId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sections.FindAsync(id, cancellationToken);
    }

    public async Task<int> UpdateAsync(Section section, CancellationToken cancellationToken = default)
    {
        _context.Sections.Update(section);
        return await _context.SaveChangesAsync(cancellationToken);
    }
}