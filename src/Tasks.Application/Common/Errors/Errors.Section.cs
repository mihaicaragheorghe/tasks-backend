using Application.Core;

namespace Application.Common.ErrorHandling;

public static partial class Errors
{
    public static class Section
    {
        public static Error FailedToCreate => Error.Failure(
            code: "Section.FailedToCreate",
            description: "Failed to create section."
        );
        public static Error FailedToUpdate => Error.Failure(
            code: "Section.FailedToUpdate",
            description: "Failed to update section."
        );
        public static Error DeleteFailed => Error.Failure(
            code: "Section.DeleteFailed",
            description: "Failed to delete section."
        );
        public static Error NotFound => Error.NotFound(
            code: "Section.NotFound",
            description: "Section not found."
        );  
        public static Error SectionNotInProject => Error.Failure(
            code: "Section.SectionNotInProject",
            description: "Task's section is not in the task project."
        );
    }
}