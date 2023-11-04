using FluentValidation;

using Tasks.Domain;

namespace Tasks.Application.TaskSections.Commands;

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
            .WithMessage("Name is required.")
            .MaximumLength(Constants.Section.NameMaxLength)
            .WithErrorCode("Section.NameMaxLength")
            .WithMessage($"Name must be less than {Constants.Section.NameMaxLength} characters.");
    }
}