using FluentValidation;

namespace Tasks.Application.Tasks.Commands;

public class CreateSubtaskCommandValidator : AbstractValidator<CreateSubtaskCommand>
{
    public CreateSubtaskCommandValidator()
    {
        RuleFor(x => x.ParentId)
            .NotEmpty()
            .WithErrorCode("Subtask.ParentIdRequired")
            .WithMessage("Parent task id is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithErrorCode("Subtask.TitleRequired")
            .WithMessage("Title is required.");
    }
}