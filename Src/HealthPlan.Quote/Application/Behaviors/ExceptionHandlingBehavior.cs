using MediatR;
using Microsoft.Extensions.Logging;

namespace HealthPlan.Quote.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior that handles exceptions and provides consistent error handling.
    /// Catches exceptions from handlers and logs them before re-throwing.
    /// </summary>
    /// <typeparam name="TRequest">The type of request</typeparam>
    /// <typeparam name="TResponse">The type of response</typeparam>
    public class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the ExceptionHandlingBehavior class.
        /// </summary>
        /// <param name="logger">Logger instance for this behavior</param>
        public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Wraps the handler execution in exception handling logic.
        /// </summary>
        /// <param name="request">The request being handled</param>
        /// <param name="next">The next handler in the pipeline</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The response from the handler</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (FluentValidation.ValidationException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Validation failed for {RequestName}: {ValidationErrors}",
                    typeof(TRequest).Name,
                    string.Join(", ", ex.Errors.Select(e => e.ErrorMessage)));

                throw;
            }
            catch (OperationCanceledException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Operation was cancelled for {RequestName}",
                    typeof(TRequest).Name);

                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Unhandled exception for {RequestName}: {Message}",
                    typeof(TRequest).Name,
                    ex.Message);

                throw;
            }
        }
    }
}
