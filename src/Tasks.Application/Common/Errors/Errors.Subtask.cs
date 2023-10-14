using Tasks.Application.Core;

namespace Tasks.Application.Common.Errors;

public static partial class Errors
{
    public static class Subtask
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Subtask.FailedToCreate",
            description: "Failed to create subtask."
        );
    }
}