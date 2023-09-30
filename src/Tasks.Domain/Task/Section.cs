namespace Tasks.Domain;

public class Section
{
    private readonly List<TaskEntity> _tasks = new();

    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }
    public int Order { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyCollection<TaskEntity> Tasks => _tasks.AsReadOnly();

    public Section(
        Guid id,
        Guid projectId,
        string name,
        int order,
        bool isDeleted,
        List<TaskEntity> tasks)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        Order = order;
        IsDeleted = isDeleted;
        _tasks = tasks;
    }

    public static Section Create(
        Guid projectId,
        string name,
        List<TaskEntity>? tasks = null)
    {
        return new Section(
            Guid.NewGuid(),
            projectId,
            name,
            0,
            false,
            tasks ?? new List<TaskEntity>());
    }
}
