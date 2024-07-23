using FluentValidation;
using MediatR;
using Rent.Service.Application.Common.Exceptions;

namespace Rent.Service.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull 
{
    private IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request,
    RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationTasks = _validators
                .Select(v => v.ValidateAsync(context, cancellationToken));

            var validationResults = await Task.WhenAll(validationTasks);

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Select(v => v.ErrorMessage)
                .Where(v => v != null)
                .ToList();

            if (failures.Any())
                throw new InvalidRequestException(failures);
        }
        return await next();
    }
}
