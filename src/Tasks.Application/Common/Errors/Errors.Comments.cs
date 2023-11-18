using Tasks.Application.Core;

namespace Tasks.Application.Common.Errors;

public static partial class Errors
{
    public static class Comments
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Comments.FailedToCreate",
            description: "Failed to create comment.");

        public static Error FailedToUpdate => Error.Failure(
            code: "Comments.FailedToUpdate",
            description: "Failed to update comment.");

        public static Error TaskIdRequired => Error.Failure(
            code: "Comments.TaskIdRequired",
            description: "Task id is required.");

        public static Error ContentRequired => Error.Failure(
            code: "Comments.ContentRequired",
            description: "Comment content is required.");
    }
}