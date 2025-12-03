using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for deleting quotes.
    /// </summary>
    public class DeleteQuoteCommandHandler : IRequestHandler<DeleteQuoteCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteQuoteCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
        {
            var quote = unitOfWork.QuoteRepository.GetById(request.Id);

            if (quote == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.QuoteRepository.Remove(quote);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(true);
        }
    }
}
