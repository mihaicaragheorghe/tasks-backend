using Microsoft.EntityFrameworkCore;
using Tasks.Application.Common.Repository;
using Tasks.Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class TagRepository : ITagRepository
{
    private readonly DataContext _context;

    public TagRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<int> AddAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        await _context.Tags.AddAsync(tag, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        _context.Tags.Update(tag);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        _context.Tags.Remove(tag);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Tag>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Tag>> GetTagsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Tags
            .Where(t => ids.Contains(t.Id))
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Tags.FindAsync(id);
    }
}