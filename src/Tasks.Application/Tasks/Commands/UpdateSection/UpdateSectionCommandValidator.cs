using FluentValidation;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class UpdateSectionCommandValidator : AbstractValidator<UpdateSectionCommand>
{
    public UpdateSectionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode("Section.IdRequired")
            .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithErrorCode("Section.NameRequired")
            .WithMessage("Name is required.")
            .MaximumLength(Constants.Section.NameMaxLength)
            .WithErrorCode("Section.NameMaxLength")
            .WithMessage($"Name must be less than {Constants.Section.NameMaxLength} characters.");
    }
}