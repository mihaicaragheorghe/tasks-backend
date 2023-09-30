using System.Net;

namespace Tasks.Application.Core;

public readonly record struct Error(
    string Code, 
    string Description, 
    ErrorType Type, 
    HttpStatusCode HttpStatusCode)
{
    public static Error Unexpected(string code = "General.Unexpected", string description = "An unexpected error occurred.") =>
        new(code, description, ErrorType.Unexpected, HttpStatusCode.InternalServerError);

    public static Error Validation(string code = "General.Validation", string description = "A validation error occurred.") =>
        new(code, description, ErrorType.Validation, HttpStatusCode.BadRequest);

    public static Error Conflict(string code = "General.Conflict", string description = "A conflict error occurred.") =>
        new(code, description, ErrorType.Conflict, HttpStatusCode.Conflict);

    public static Error NotFound(string code = "General.NotFound", string description = "A not found error occurred.") =>
        new(code, description, ErrorType.NotFound, HttpStatusCode.NotFound);

    public static Error NotAuthorized(string code = "General.NotAuthorized", string description = "A not authorized error occurred.") =>
        new(code, description, ErrorType.NotAuthorized, HttpStatusCode.Unauthorized);

    public static Error Failure(string code = "General.Failure", string description = "A failure error occurred.") =>
        new(code, description, ErrorType.Failure, HttpStatusCode.InternalServerError);
}
