using Tasks.Domain;

namespace Tasks.Application.Common.Repository;

public interface ITagRepository
{
    Task<IEnumerable<Tag>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Tag>> GetTagsByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Tag tag, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(Tag tag, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Tag tag, CancellationToken cancellationToken = default);
}