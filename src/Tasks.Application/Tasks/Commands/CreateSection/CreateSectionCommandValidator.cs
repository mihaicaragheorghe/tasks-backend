using FluentValidation;
using Tasks.Application.Common.Errors;

namespace Tasks.Application.Tasks.Commands;

public class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
{
    public CreateSectionCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithErrorCode("Section.ProjectIdRequired")
            .WithMessage("Project id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("Section.NameRequired")
            .WithMessage("Name is required.");
    }
}