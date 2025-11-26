using MediatR;
using Microsoft.Extensions.Logging;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Application.Behaviors
{
    /// <summary>
    /// Marker interface to identify commands that require transaction handling.
    /// Commands implementing this interface will be wrapped in a database transaction.
    /// </summary>
    public interface ITransactionalRequest
    {
    }

    /// <summary>
    /// Pipeline behavior that wraps command execution in a database transaction.
    /// Only applies to requests that implement ITransactionalRequest marker interface.
    /// </summary>
    /// <typeparam name="TRequest">The type of request</typeparam>
    /// <typeparam name="TResponse">The type of response</typeparam>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IApplicationUnitOfWork _unitOfWork;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        /// <summary>
        /// Initializes a new instance of the TransactionBehavior class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="logger">Logger instance for this behavior</param>
        public TransactionBehavior(
            IApplicationUnitOfWork unitOfWork,
            ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Wraps the handler execution in a transaction if the request implements ITransactionalRequest.
        /// </summary>
        /// <param name="request">The request being handled</param>
        /// <param name="next">The next handler in the pipeline</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The response from the handler</returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Only wrap in transaction if the request implements ITransactionalRequest
            if (request is not ITransactionalRequest)
            {
                return await next();
            }

            var requestName = typeof(TRequest).Name;

            _logger.LogInformation(
                "Beginning transaction for {RequestName}",
                requestName);

            try
            {
                var response = await next();

                await _unitOfWork.CompleteAsync();

                _logger.LogInformation(
                    "Transaction committed for {RequestName}",
                    requestName);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Transaction rolled back for {RequestName}: {Message}",
                    requestName,
                    ex.Message);

                throw;
            }
        }
    }
}
