using System.Text.RegularExpressions;

using FluentValidation;

namespace Application.Projects.Commands;

public partial class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    [GeneratedRegex("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    private static partial Regex HexColorCodeRegex();

    public CreateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Domain.Constants.Project.NameMaxLength)
            .WithErrorCode("Project.NameLength")
            .WithMessage("Project name must be between 1 and 50 characters.");

        RuleFor(x => x.Color)
            .Must(x => x == null || HexColorCodeRegex().IsMatch(x))
            .WithErrorCode("Project.ColorFormat")
            .WithMessage("Project color must be a valid hex color code or null.");
    }
}