using Domain;

namespace Tasks.Api.Contracts;

public record TagDto(Guid Id, string Name, string? Color)
{
    public TagDto(Tag tag)
        : this(
            tag.Id,
            tag.Name,
            tag.Color) { }
}