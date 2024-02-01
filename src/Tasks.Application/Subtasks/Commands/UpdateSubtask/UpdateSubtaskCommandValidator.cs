using FluentValidation;

namespace Application.Subtasks.Commands
{
    public class UpdateSubtaskCommandValidator : AbstractValidator<UpdateSubtaskCommand>
    {
        public UpdateSubtaskCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithErrorCode("Subtask.IdRequired")
                .WithMessage("Id is required.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithErrorCode("Subtask.TitleRequired")
                .WithMessage("Title is required.")
                .MaximumLength(100)
                .WithErrorCode("Subtask.TitleMaxLength")
                .WithMessage("Title must be less than 100 characters.");
        }
    }
}