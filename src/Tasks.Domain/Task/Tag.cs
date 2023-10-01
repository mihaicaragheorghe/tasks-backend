namespace Tasks.Domain;

public class Tag
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string? Color { get; private set; }
    
    public Guid UserId { get; private set; }

    public Tag(
        Guid id,
        Guid userId,
        string name,
        string? color = null)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Color = color;
    }

    public static Tag Create(
        Guid userId,
        string name,
        string? color = null)
    {
        return new Tag(
            Guid.NewGuid(),
            userId,
            name,
            color);
    }

    private Tag() { }
}
