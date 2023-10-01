namespace Tasks.Domain;

public class Comment
{
    public Guid Id { get; private set; }
    public string Content { get; private set; } = null!;
    public DateTime CreatedAtUtc { get; private set; }

    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Comment(
        Guid id,
        Guid taskId,
        Guid userId,
        string content,
        DateTime createdAt)
    {
        Id = id;
        TaskId = taskId;
        UserId = userId;
        Content = content;
        CreatedAtUtc = createdAt;
    }
    
    public static Comment Create(
        Guid taskId,
        Guid userId,
        string content)
    {
        return new Comment(
            Guid.NewGuid(),
            taskId,
            userId,
            content,
            DateTime.UtcNow);
    }

    private Comment() { }
}