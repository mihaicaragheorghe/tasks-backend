using FluentValidation;
using Domain;

namespace Application.Tasks.Commands;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithErrorCode("Task.ProjectIdRequired")
            .WithMessage("ProjectId is required.");

        RuleFor(x => x.AssignedToUserId)
            .NotEmpty()
            .WithErrorCode("Task.AssignedToUserIdRequired")
            .WithMessage("AssignedToUserId is required.");

        RuleFor(x => x.CreatedByUserId)
            .NotEmpty()
            .WithErrorCode("Task.CreatedByUserIdRequired")
            .WithMessage("CreatedByUserId is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithErrorCode("Task.TitleRequired")
            .WithMessage("Title is required.")
            .MaximumLength(Constants.Task.TitleMaxLength)
            .WithErrorCode("Task.TitleMaxLength")
            .WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(Constants.Task.DescriptionMaxLength)
            .WithErrorCode("Task.DescriptionMaxLength")
            .WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithErrorCode("Task.PriorityInvalid")
            .WithMessage("Priority is invalid.");

        RuleFor(x => x.DueAtUtc)
            .Must(x => x == null || x > DateTime.UtcNow)
            .WithErrorCode("Task.DueAtUtcInvalid")
            .WithMessage("DueAtUtc must be in the future or null.");
    }
}