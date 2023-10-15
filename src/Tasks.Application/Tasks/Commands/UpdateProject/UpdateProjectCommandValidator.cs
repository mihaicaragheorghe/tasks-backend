using FluentValidation;

namespace Tasks.Application.Tasks.Commands;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Color)
            .MaximumLength(10);

        RuleFor(x => x.Order)
            .GreaterThanOrEqualTo(0);
    }
}