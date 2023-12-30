using Tasks.Application.Core;

namespace Tasks.Application.Common.ErrorHandling;

public static partial class Errors
{
    public static class Project
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Project.FailedToCreate",
            description: "Failed to create project."
        );
        public static Error FailedToUpdate => Error.Failure(
            code: "Project.FailedToUpdate",
            description: "Failed to update project."
        );
        public static Error DeleteFailed => Error.Failure(
            code: "Project.DeleteFailed",
            description: "Failed to delete project."
        );
        public static Error NotFound => Error.NotFound(
            code: "Project.NotFound",
            description: "Project not found."
        );
    }
}