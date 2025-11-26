using FluentValidation;
using MediatR;

namespace HealthPlan.Quote.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior that validates requests before they reach the handler.
    /// Uses FluentValidation to perform validation on request objects.
    /// </summary>
    /// <typeparam name="TRequest">The type of request being validated</typeparam>
    /// <typeparam name="TResponse">The type of response</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        /// <summary>
        /// Initializes a new instance of the ValidationBehavior class.
        /// </summary>
        /// <param name="validators">Collection of validators for the request type</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        /// <summary>
        /// Validates the request before passing it to the next handler in the pipeline.
        /// </summary>
        /// <param name="request">The request to validate</param>
        /// <param name="next">The next handler in the pipeline</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The response from the handler</returns>
        /// <exception cref="ValidationException">Thrown when validation fails</exception>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}
