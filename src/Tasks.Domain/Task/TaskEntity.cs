namespace Tasks.Domain;

public class TaskEntity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public TaskPriority Priority { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }
    public DateTime? DueAtUtc { get; private set; }
    public bool IsDeleted { get; private set; }

    public Guid CreatedByUserId { get; private set; }
    public User CreatedBy { get; private set; } = null!;
    public Guid AssignedToUserId { get; private set; }
    public User AssignedTo { get; private set; } = null!;
    public Guid ProjectId { get; private set; }
    public Guid? SectionId { get; private set; }

    public ICollection<Tag> Tags { get; private set; } = new List<Tag>();
    public ICollection<Subtask> Subtasks { get; private set; } = new List<Subtask>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

    public TaskEntity(
        Guid id,
        Guid createdBy,
        Guid assignedTo,
        Guid projectId,
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
        ProjectId = projectId;
        SectionId = sectionId;
        Title = title;
        Description = description;
        Priority = priority;
        IsComplete = isComplete;
        CreatedAtUtc = createdAt;
        CompletedAtUtc = completedAt;
        DueAtUtc = dueAt;
        IsDeleted = isDeleted;
        Tags = tags;
        Subtasks = subtasks;
        Comments = comments;
    }

    public static TaskEntity Create(
        Guid createdBy,
        Guid assignedTo,
        Guid projectId,
        Guid? sectionId,
        string title,
        string? description,
        TaskPriority priority,
        DateTime? dueAt = null,
        List<Tag>? tags = null,
        List<Subtask>? subtasks = null,
        List<Comment>? comments = null)
    {
        return new TaskEntity(
            Guid.NewGuid(),
            createdBy,
            assignedTo,
            projectId,
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

    private TaskEntity() { }
}