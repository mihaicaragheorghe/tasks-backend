using Tasks.Domain;

public interface ITaskRepository
{
    Task<IEnumerable<TaskEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<int> AddAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task<int> UpdateAsync(TaskEntity task, CancellationToken cancellationToken = default);
    Task<int> DeleteAsync(TaskEntity task, CancellationToken cancellationToken = default);
}