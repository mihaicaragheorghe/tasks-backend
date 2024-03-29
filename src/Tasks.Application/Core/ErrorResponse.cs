using FluentValidation;

namespace Application.Core;

public class ErrorResponse
{
    private readonly List<ErrorDto> _errors = [];

    public IReadOnlyCollection<ErrorDto> Errors => _errors.AsReadOnly();

    public ErrorResponse(List<ErrorDto> errors)
    {
        _errors = errors;
    }

    public ErrorResponse(Error error) => 
        _errors = [ new(error) ];

    public ErrorResponse(ServiceException exception) : this(exception.Error) { }

    public ErrorResponse(ValidationException exception) 
        : this(exception.Errors.Select(x => new ErrorDto(x.ErrorCode, x.ErrorMessage)).ToList()) { }
        
    public static ErrorResponse InternalServerError() => new(Error.Unexpected());
}
