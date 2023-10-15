using System.Text.RegularExpressions;

using FluentValidation;

namespace Tasks.Application.Tasks.Commands;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Domain.Constants.Project.NameMaxLength)
            .WithErrorCode("Project.NameLength")
            .WithMessage("Project name must be between 1 and 50 characters.");

        RuleFor(x => x.Color)
            .Must(x => x == null || Regex.IsMatch(x, "^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$"))
            .WithErrorCode("Project.ColorFormat")
            .WithMessage("Project color must be a valid hex color code or null.");
    }
}