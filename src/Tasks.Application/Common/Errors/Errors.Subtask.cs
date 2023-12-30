using Tasks.Application.Core;

namespace Tasks.Application.Common.ErrorHandling;

public static partial class Errors
{
    public static class Subtask
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Subtask.FailedToCreate",
            description: "Failed to create subtask."
        );
        public static Error FailedToUpdate => Error.Failure(
            code: "Subtask.FailedToUpdate",
            description: "Failed to update subtask."
        );
        public static Error FailedToDelete => Error.Failure(
            code: "Subtask.FailedToDelete",
            description: "Failed to delete subtask."
        );
        public static Error NotFound => Error.NotFound(
            code: "Subtask.NotFound",
            description: "Subtask not found."
        );
    }
}