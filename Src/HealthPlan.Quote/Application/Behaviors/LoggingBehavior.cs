using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HealthPlan.Quote.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior that logs all requests and their execution time.
    /// Provides centralized logging for debugging and monitoring.
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            
            logger.LogInformation("Handling {RequestName}", requestName);
            
            var stopwatch = Stopwatch.StartNew();
            
            try
            {
                var response = await next().ConfigureAwait(false);
                
                stopwatch.Stop();
                
                logger.LogInformation(
                    "Handled {RequestName} in {ElapsedMilliseconds}ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);
                
                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                
                logger.LogError(
                    ex,
                    "Error handling {RequestName} after {ElapsedMilliseconds}ms",
                    requestName,
                    stopwatch.ElapsedMilliseconds);
                
                throw;
            }
        }
    }
}
