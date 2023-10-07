using Tasks.Application.Core;

namespace Tasks.Api;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.NotAuthorized(
            code: "Authentication.InvalidCredentials",
            description: "Invalid credentials."
        );
        public static Error FailedToCreateRefreshToken => Error.Failure(
            code: "Authentication.FailedToCreateRefreshToken",
            description: "Failed to create a new refresh token."
        );
    }
}
