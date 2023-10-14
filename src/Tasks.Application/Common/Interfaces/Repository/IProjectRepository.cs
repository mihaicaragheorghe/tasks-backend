using Tasks.Domain;

namespace Tasks.Application.Common.Repository;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Project?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Project project, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(Project project, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Project project, CancellationToken cancellationToken = default);
}