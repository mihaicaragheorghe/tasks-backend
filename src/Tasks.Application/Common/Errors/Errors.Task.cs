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
        public static Error FailedToUpdate => Error.Failure(
            code: "Task.FailedToUpdate",
            description: "Failed to update task."
        );
        public static Error FailedToDelete => Error.Failure(
            code: "Task.FailedToDelete",
            description: "Failed to delete task."
        );
        public static Error NotFound => Error.Failure(
            code: "Task.NotFound",
            description: "Task not found."
        );
    }
}