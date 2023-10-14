using Tasks.Application.Core;

namespace Tasks.Application.Common.Errors;

public static partial class Errors
{
    public static class Section
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Section.FailedToCreate",
            description: "Failed to create section."
        );
    }
}