using Tasks.Domain;

namespace Tasks.Application.Common.Repository;

public interface ISectionRepository
{
    Task<List<Section>> GetAllAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Section section, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(Section section, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Section section, CancellationToken cancellationToken = default);
}