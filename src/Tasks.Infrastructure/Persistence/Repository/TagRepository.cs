using Microsoft.EntityFrameworkCore;
using Application.Common.Repository;
using Domain;

namespace Tasks.Infrastructure.Persistence.Repository;

public class TagRepository(DataContext context) : ITagRepository
{
    public async Task<int> AddAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        await context.Tags.AddAsync(tag, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        context.Tags.Update(tag);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Tag tag, CancellationToken cancellationToken = default)
    {
        context.Tags.Remove(tag);
        return await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Tag>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Tags
            .Where(t => t.UserId == userId)
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Tag>> GetTagsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await context.Tags
            .Where(t => ids.Contains(t.Id))
            .OrderBy(t => t.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Tags.FindAsync(id);
    }
}