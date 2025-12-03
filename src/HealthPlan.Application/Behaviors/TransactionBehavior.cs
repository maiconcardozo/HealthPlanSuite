using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior that wraps command execution in a database transaction.
    /// Automatically commits on success and rolls back on exception.
    /// Only applies to commands (requests that end with "Command").
    /// </summary>
    /// <typeparam name="TRequest">The request type.</typeparam>
    /// <typeparam name="TResponse">The response type.</typeparam>
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public TransactionBehavior(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            // Only apply transaction to commands (not queries)
            if (!requestName.EndsWith("Command", StringComparison.OrdinalIgnoreCase))
            {
                return await next().ConfigureAwait(false);
            }

            var response = await next().ConfigureAwait(false);
            
            // Commit transaction after successful command execution
            await unitOfWork.CompleteAsync().ConfigureAwait(false);
            
            return response;
        }
    }
}
