namespace Tasks.Domain;

public class UserComment
{
    public Guid Id { get; private set; }
    public Guid TaskItemId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public UserComment(
        Guid id,
        Guid taskItemId,
        string content,
        DateTime createdAt)
    {
        Id = id;
        TaskItemId = taskItemId;
        Content = content;
        CreatedAtUtc = createdAt;
    }
    
    public static UserComment Create(
        Guid taskItemId,
        string content)
    {
        return new UserComment(
            Guid.NewGuid(),
            taskItemId,
            content,
            DateTime.UtcNow);
    }
}