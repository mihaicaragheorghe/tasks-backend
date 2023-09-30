namespace Tasks.Domain;

public class TaskItem
{
    private readonly List<TaskTag> _tags = new List<TaskTag>();
    private readonly List<Subtask> _subtasks = new List<Subtask>();
    private readonly List<UserComment> _comments = new List<UserComment>();

    public Guid Id { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public Guid AssignedToUserId { get; private set; }
    public Guid SectionId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskPriority Priority { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }
    public DateTime? DueAtUtc { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyCollection<TaskTag> Tags => _tags.AsReadOnly();
    public IReadOnlyCollection<Subtask> Subtasks => _subtasks.AsReadOnly();
    public IReadOnlyCollection<UserComment> Comments => _comments.AsReadOnly();

    public TaskItem(
        Guid id,
        Guid createdBy,
        Guid assignedTo,
        Guid sectionId,
        string title,
        string? description,
        TaskPriority priority,
        bool isComplete,
        DateTime createdAt,
        DateTime? completedAt,
        DateTime? dueAt,
        bool isDeleted,
        List<TaskTag> tags,
        List<Subtask> subtasks,
        List<UserComment> comments)
    {
        Id = id;
        CreatedByUserId = createdBy;
        AssignedToUserId = assignedTo;
        SectionId = sectionId;
        Title = title;
        Description = description;
        Priority = priority;
        IsComplete = isComplete;
        CreatedAtUtc = createdAt;
        CompletedAtUtc = completedAt;
        DueAtUtc = dueAt;
        IsDeleted = isDeleted;
        _tags = tags;
        _subtasks = subtasks;
        _comments = comments;
    }

    public static TaskItem Create(
        Guid createdBy,
        Guid assignedTo,
        Guid sectionId,
        string title,
        string? description,
        TaskPriority priority,
        DateTime? dueAt,
        List<TaskTag>? tags = null,
        List<Subtask>? subtasks = null,
        List<UserComment>? comments = null)
    {
        return new TaskItem(
            Guid.NewGuid(),
            createdBy,
            assignedTo,
            sectionId,
            title,
            description,
            priority,
            false,
            DateTime.UtcNow,
            null,
            dueAt,
            false,
            tags ?? new List<TaskTag>(),
            subtasks ?? new List<Subtask>(),
            comments ?? new List<UserComment>());
    }
}