namespace Tasks.Domain;

public class Section
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public int Order { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public Guid ProjectId { get; private set; }
    public ICollection<TaskEntity> Tasks { get; private set; } = new List<TaskEntity>();

    public Section(
        Guid id,
        Guid projectId,
        string name,
        int order,
        bool isDeleted,
        DateTime createdAtUtc)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        Order = order;
        IsDeleted = isDeleted;
        CreatedAtUtc = createdAtUtc;
    }

    public static Section Create(
        Guid projectId,
        string name)
    {
        return new Section(
            Guid.NewGuid(),
            projectId,
            name,
            0,
            false,
            DateTime.UtcNow);
    }
    
    private Section() { }
}
