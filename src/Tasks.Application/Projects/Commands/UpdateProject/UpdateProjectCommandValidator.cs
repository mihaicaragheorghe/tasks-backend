using System.Text.RegularExpressions;

using FluentValidation;

using Domain;

namespace Application.Projects.Commands;

public partial class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    [GeneratedRegex("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$")]
    private static partial Regex HexColorCodeRegex();

    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Constants.Project.NameMaxLength)
            .WithErrorCode("Project.NameLength")
            .WithMessage("Project name must be between 1 and 50 characters.");

        RuleFor(x => x.Color)
            .Must(x => x == null || HexColorCodeRegex().IsMatch(x))
            .WithErrorCode("Project.ColorLength")
            .WithMessage("Project color must be a valid hex color code or null.");
    }
}