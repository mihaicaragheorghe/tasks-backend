using Tasks.Application.Core;

namespace Tasks.Application.Tasks;

public static partial class Errors
{
    public static class Project
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Project.FailedToCreate",
            description: "Failed to create project."
        );
    }
}