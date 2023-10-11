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
        public static Error EmailExists => Error.Conflict(
            code: "Authentication.EmailExists",
            description: "Email already exists."
        );
        public static Error UsernameExists => Error.Conflict(
            code: "Authentication.UsernameExists",
            description: "Username already exists."
        );
        public static Error FailedToCreateUser => Error.Failure(
            code: "Authentication.FailedToCreateUser",
            description: "Failed to create user."
        );
        public static Error Unauthorized => Error.NotAuthorized(
            code: "Authentication.Unauthorized",
            description: "Unauthorized."
        );
        public static Error InvalidRefreshToken => Error.NotAuthorized(
            code: "Authentication.InvalidRefreshToken",
            description: "Invalid refresh token."
        );
        public static Error RefreshTokenNotFound => Error.NotFound(
            code: "Authentication.RefreshTokenNotFound",
            description: "Refresh token not found."
        );
        public static Error UserNotFound => Error.NotFound(
            code: "Authentication.UserNotFound",
            description: "User not found."
        );
    }
}
