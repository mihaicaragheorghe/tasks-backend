namespace Tasks.Domain;

public class Comment
{
    public Guid Id { get; private set; }
    public Guid TaskId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public Comment(
        Guid id,
        Guid taskId,
        string content,
        DateTime createdAt)
    {
        Id = id;
        TaskId = taskId;
        Content = content;
        CreatedAtUtc = createdAt;
    }
    
    public static Comment Create(
        Guid taskId,
        string content)
    {
        return new Comment(
            Guid.NewGuid(),
            taskId,
            content,
            DateTime.UtcNow);
    }
}