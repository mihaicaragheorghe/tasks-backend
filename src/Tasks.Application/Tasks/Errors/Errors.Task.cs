using Tasks.Application.Core;

namespace Tasks.Domain;

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