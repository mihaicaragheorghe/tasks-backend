using FluentValidation;

using Application.Common.ErrorHandling;

namespace Application.Comments.Commands;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.TaskId)
            .NotEmpty()
            .WithErrorCode(Errors.Comments.TaskIdRequired.Code)
            .WithMessage(Errors.Comments.TaskIdRequired.Description);

        RuleFor(x => x.Content)
            .NotEmpty()
            .WithErrorCode(Errors.Comments.ContentRequired.Code)
            .WithMessage(Errors.Comments.ContentRequired.Description);
    }
}