namespace Tasks.Domain;

public class TaskTag
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Color { get; private set; }

    public TaskTag(
        Guid id,
        string name,
        string color)
    {
        Id = id;
        Name = name;
        Color = color;
    }

    public static TaskTag Create(
        string name,
        string color)
    {
        return new TaskTag(
            Guid.NewGuid(),
            name,
            color);
    }
}
