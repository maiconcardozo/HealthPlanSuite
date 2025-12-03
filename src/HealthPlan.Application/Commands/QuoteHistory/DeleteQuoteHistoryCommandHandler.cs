using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteQuoteHistoryCommandHandler : IRequestHandler<DeleteQuoteHistoryCommand, bool>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public DeleteQuoteHistoryCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<bool> Handle(DeleteQuoteHistoryCommand request, CancellationToken cancellationToken)
        {
            var quoteHistory = unitOfWork.QuoteHistoryRepository.GetById(request.Id);

            if (quoteHistory == null)
            {
                return Task.FromResult(false);
            }

            unitOfWork.QuoteHistoryRepository.Remove(quoteHistory);

            return Task.FromResult(true);
        }
    }
}
