namespace Tasks.Domain;

public class TaskSection
{
    private readonly List<Task> _tasks = new();

    public Guid Id { get; private set; }
    public Guid ProjectId { get; private set; }
    public string Name { get; private set; }
    public int Order { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyList<Task> Tasks => _tasks.AsReadOnly();

    public TaskSection(
        Guid id,
        Guid projectId,
        string name,
        int order,
        bool isDeleted,
        List<Task> tasks)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        Order = order;
        IsDeleted = isDeleted;
        _tasks = tasks;
    }

    public static TaskSection Create(
        Guid projectId,
        string name,
        List<Task>? tasks = null)
    {
        return new TaskSection(
            Guid.NewGuid(),
            projectId,
            name,
            0,
            false,
            tasks ?? new List<Task>());
    }
}
