using Tasks.Domain;

namespace Tasks.Application.Common.Repository;

public interface ITagRepository
{
    Task<List<Tag>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<List<Tag>> GetTagsByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
    Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Tag tag, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(Tag tag, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Tag tag, CancellationToken cancellationToken = default);
}