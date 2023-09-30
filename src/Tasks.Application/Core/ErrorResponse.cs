using FluentValidation;

namespace Tasks.Application.Core;

public class ErrorResponse
{
    private readonly List<ErrorDto> _errors = new();

    public IReadOnlyCollection<ErrorDto> Errors => _errors.AsReadOnly();

    public ErrorResponse(List<ErrorDto> errors)
    {
        _errors = errors;
    }

    public ErrorResponse(Error error) => 
        _errors = new List<ErrorDto> { new(error) };

    public ErrorResponse(AppException exception) : this(exception.Error) { }

    public ErrorResponse(ValidationException exception) 
        : this(exception.Errors.Select(x => new ErrorDto(x.ErrorCode, x.ErrorMessage)).ToList()) { }
        
    public static ErrorResponse InternalServerError() => new(Error.Unexpected());
}
