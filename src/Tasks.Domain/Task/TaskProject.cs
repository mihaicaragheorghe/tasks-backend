namespace Tasks.Domain;

public class TaskProject
{
    private readonly List<TaskSection> _sections = new();

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }
    public int Order { get; private set; }
    public bool IsArchived { get; private set; }
    public bool IsFavorite { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyList<TaskSection> Sections => _sections.AsReadOnly();

    public TaskProject(
        Guid id, 
        string name, 
        string color, 
        int order,
        bool isArchived, 
        bool isFavorite, 
        bool isDeleted,
        List<TaskSection> sections)
    {
        Id = id;
        Name = name;
        Color = color;
        Order = order;
        IsArchived = isArchived;
        IsFavorite = isFavorite;
        IsDeleted = isDeleted;
        _sections = sections;
    }

    public static TaskProject Create(
        string name, 
        string color, 
        List<TaskSection>? sections = null)
    {
        return new TaskProject(
            Guid.NewGuid(), 
            name, 
            color, 
            0,
            false, 
            false, 
            false,
            sections ?? new List<TaskSection>());
    }
}
