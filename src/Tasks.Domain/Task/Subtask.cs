namespace Tasks.Domain;

public class Subtask
{
    public Guid Id { get; private set; }
    public Guid TaskItemId { get; private set; }
    public string Title { get; private set; }
    public bool IsComplete { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? CompletedAtUtc { get; private set; }

    public Subtask(
        Guid id,
        Guid taskItemId,
        string title,
        bool isComplete,
        DateTime createdAt,
        DateTime? completedAt)
    {
        Id = id;
        TaskItemId = taskItemId;
        Title = title;
        IsComplete = isComplete;
        CreatedAtUtc = createdAt;
        CompletedAtUtc = completedAt;
    }
    
    public static Subtask Create(
        Guid taskItemId,
        string title)
    {
        return new Subtask(
            Guid.NewGuid(),
            taskItemId,
            title,
            false,
            DateTime.UtcNow,
            null);
    }
}