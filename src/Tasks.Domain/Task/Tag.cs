namespace Tasks.Domain;

public class Tag
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Color { get; private set; }

    public Tag(
        Guid id,
        string name,
        string? color = null)
    {
        Id = id;
        Name = name;
        Color = color;
    }

    public static Tag Create(
        string name,
        string? color = null)
    {
        return new Tag(
            Guid.NewGuid(),
            name,
            color);
    }
}
