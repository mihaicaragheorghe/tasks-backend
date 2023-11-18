namespace Tasks.Application.Core;

public readonly record struct ErrorDto(
    string Code,
    string Description)
{
    public ErrorDto(Error error) : this(error.Code, error.Description) { }
}
