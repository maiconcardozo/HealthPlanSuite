using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HealthPlan.Quote.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior that logs request handling information including execution time.
    /// Provides debugging and monitoring capabilities for all MediatR requests.
    /// </summary>
    /// <typeparam name="TRequest">The type of request being logged</typeparam>
    /// <typeparam name="TResponse">The type of response</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the LoggingBehavior class.
        /// </summary>
        /// <param name="logger">Logger instance for this behavior</param>
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logs request details before and after handler execution.
        /// </summary>
        /// <param name="request">The request being handled</param>
        /// <param name="next">The next handler in the pipeline</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The response from the handler</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestGuid = Guid.NewGuid().ToString();

            _logger.LogInformation(
                "[START] Request {RequestName} ({RequestGuid}) - {Timestamp}",
                requestName,
                requestGuid,
                DateTime.UtcNow);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();

                _logger.LogInformation(
                    "[END] Request {RequestName} ({RequestGuid}) - Elapsed: {ElapsedMilliseconds}ms",
                    requestName,
                    requestGuid,
                    stopwatch.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(
                    ex,
                    "[ERROR] Request {RequestName} ({RequestGuid}) - Elapsed: {ElapsedMilliseconds}ms - Error: {ErrorMessage}",
                    requestName,
                    requestGuid,
                    stopwatch.ElapsedMilliseconds,
                    ex.Message);

                throw;
            }
        }
    }
}
