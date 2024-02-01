using FluentValidation;

using MediatR;

namespace Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));

        var errors = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        return errors.Count != 0 ? throw new ValidationException(errors) : await next();
    }
}