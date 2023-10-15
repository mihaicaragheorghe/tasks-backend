using Tasks.Domain;

namespace Tasks.Application.Common.Repository;

public interface ISubtaskRepository
{
    Task<List<Subtask>> GetByTaskIdAsync(Guid taskId, CancellationToken cancellationToken = default);
    Task<Subtask?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(Subtask subtask, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(Subtask subtask, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(Subtask subtask, CancellationToken cancellationToken = default);
}