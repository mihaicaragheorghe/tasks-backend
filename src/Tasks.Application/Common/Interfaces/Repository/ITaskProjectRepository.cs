using Tasks.Domain;

namespace Tasks.Application.Common.Repository;

public interface ITaskProjectRepository
{
    Task<IEnumerable<TaskProject>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskProject> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(TaskProject project, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(TaskProject project, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(TaskProject project, CancellationToken cancellationToken = default);
}