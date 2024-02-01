namespace Domain;

public class Project
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Color { get; private set; }
    public int OrderIndex { get; private set; }
    public bool IsArchived { get; private set; }
    public bool IsFavorite { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }

    public Guid OwnerId { get; private set; }
    public ICollection<Section> Sections { get; private set; } = new List<Section>();
    public ICollection<TaskEntity> Tasks { get; private set; } = new List<TaskEntity>();
    public ICollection<User> Collaborators { get; private set; } = new List<User>();


    public Project(
        Guid id,
        string name, 
        string? color, 
        int order,
        bool isArchived, 
        bool isFavorite, 
        DateTime createdAt,
        Guid ownerId,
        List<Section> sections,
        List<TaskEntity> tasks,
        List<User> collaborators)
    {
        Id = id;
        Name = name;
        Color = color;
        OrderIndex = order;
        IsArchived = isArchived;
        IsFavorite = isFavorite;
        CreatedAtUtc = createdAt;
        OwnerId = ownerId;
        Sections = sections;
        Tasks = tasks;
        Collaborators = collaborators;
    }

    public static Project Create(
        string name, 
        Guid ownerId,
        string? color = null,
        List<Section>? sections = null,
        List<TaskEntity>? tasks = null,
        List<User>? collaborators = null)
    {
        return new Project(
            Guid.NewGuid(),
            name, 
            color, 
            0,
            false, 
            false,
            DateTime.UtcNow,
            ownerId,
            sections ?? [],
            tasks ?? [],
            collaborators ?? []);
    }

    public void Update(
        string name, 
        string? color, 
        int order,
        bool isArchived, 
        bool isFavorite)
    {
        Name = name;
        Color = color;
        OrderIndex = order;
        IsArchived = isArchived;
        IsFavorite = isFavorite;
    }

    private Project() { }
}
