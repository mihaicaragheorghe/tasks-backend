namespace Tasks.Domain;

public class Section
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public int OrderIndex { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public Guid ProjectId { get; private set; }
    public ICollection<TaskEntity> Tasks { get; private set; } = new List<TaskEntity>();

    public Section(
        Guid id,
        Guid projectId,
        string name,
        int order,
        DateTime createdAtUtc)
    {
        Id = id;
        ProjectId = projectId;
        Name = name;
        OrderIndex = order;
        CreatedAtUtc = createdAtUtc;
    }

    public Section Update(string name, int orderIndex)
    {
        Name = name;
        OrderIndex = orderIndex;

        return this;
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
            DateTime.UtcNow);
    }
    
    private Section() { }
}
