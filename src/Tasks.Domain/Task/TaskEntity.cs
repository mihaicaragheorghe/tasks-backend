namespace Tasks.Domain;

public class TaskEntity
{
    private readonly List<Tag> _tags = new List<Tag>();
    private readonly List<Subtask> _subtasks = new List<Subtask>();
    private readonly List<Comment> _comments = new List<Comment>();

    public Guid Id { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public Guid AssignedToUserId { get; private set; }
    public Guid? SectionId { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskPriority Priority { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }
    public DateTime? DueAtUtc { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    public IReadOnlyCollection<Subtask> Subtasks => _subtasks.AsReadOnly();
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    public TaskEntity(
        Guid id,
        Guid createdBy,
        Guid assignedTo,
        Guid? sectionId,
        string title,
        string? description,
        TaskPriority priority,
        bool isComplete,
        DateTime createdAt,
        DateTime? completedAt,
        DateTime? dueAt,
        bool isDeleted,
        List<Tag> tags,
        List<Subtask> subtasks,
        List<Comment> comments)
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

    public static TaskEntity Create(
        Guid createdBy,
        Guid assignedTo,
        Guid? sectionId,
        string title,
        string? description,
        TaskPriority priority,
        DateTime? dueAt,
        List<Tag>? tags = null,
        List<Subtask>? subtasks = null,
        List<Comment>? comments = null)
    {
        return new TaskEntity(
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
            tags ?? new List<Tag>(),
            subtasks ?? new List<Subtask>(),
            comments ?? new List<Comment>());
    }
}