using FluentValidation;

using Tasks.Domain;

namespace Tasks.Application.Tasks.Commands;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithErrorCode("Task.IdRequired")
            .WithMessage("Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithErrorCode("Task.TitleRequired")
            .WithMessage("Title is required.")
            .MaximumLength(Constants.Task.TitleMaxLength)
            .WithErrorCode("Task.TitleMaxLength")
            .WithMessage($"Title must be less than {Constants.Task.TitleMaxLength} characters.");

        RuleFor(x => x.Description)
            .MaximumLength(Constants.Task.DescriptionMaxLength)
            .WithErrorCode("Task.DescriptionMaxLength")
            .WithMessage($"Description must be less than {Constants.Task.DescriptionMaxLength} characters.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithErrorCode("Task.PriorityInvalid")
            .WithMessage("Priority must be a valid value.");

        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithErrorCode("Task.ProjectIdRequired")
            .WithMessage("ProjectId is required.");
    }
}