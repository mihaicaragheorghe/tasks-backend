using Tasks.Application.Core;

namespace Tasks.Application.Common.Errors;

public static partial class Errors
{
    public static class Task
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Task.FailedToCreate",
            description: "Failed to create task."
        );
    }
}