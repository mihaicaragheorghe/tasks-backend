using FluentValidation.Results;

namespace Tasks.Application.Core;

public record ErrorDto(
    string Code,
    string Description)
{
    public ErrorDto(Error error) : this(error.Code, error.Description) { }
}
