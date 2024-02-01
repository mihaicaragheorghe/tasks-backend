namespace Domain;

public class Subtask
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public bool IsComplete { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }

    public Guid ParentId { get; private set; }

    public Subtask(
        Guid id,
        Guid parentId,
        string title,
        bool isComplete,
        DateTime createdAt,
        DateTime? completedAt)
    {
        Id = id;
        ParentId = parentId;
        Title = title;
        IsComplete = isComplete;
        CreatedAtUtc = createdAt;
        CompletedAtUtc = completedAt;
    }

    public void Update(
        string title,
        bool isComplete)
    {
        Title = title;
        IsComplete = isComplete;
        CompletedAtUtc = isComplete ? DateTime.UtcNow : null;
    }
    
    public static Subtask Create(
        Guid parentId,
        string title)
    {
        return new Subtask(
            Guid.NewGuid(),
            parentId,
            title,
            false,
            DateTime.UtcNow,
            null);
    }

    private Subtask() { }
}