using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateQuoteHistoryCommandHandler : IRequestHandler<UpdateQuoteHistoryCommand, QuoteHistoryResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateQuoteHistoryCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<QuoteHistoryResponseDTO?> Handle(UpdateQuoteHistoryCommand request, CancellationToken cancellationToken)
        {
            var quoteHistory = unitOfWork.QuoteHistoryRepository.GetById(request.Id);

            if (quoteHistory == null)
            {
                return Task.FromResult<QuoteHistoryResponseDTO?>(null);
            }

            quoteHistory.IdQuote = request.QuoteId;
            quoteHistory.PreviousStatus = request.PreviousStatus;
            quoteHistory.NewStatus = request.NewStatus;
            quoteHistory.Reason = request.Reason;
            quoteHistory.Observations = request.Observations;
            quoteHistory.ChangeDate = request.ChangeDate;
            quoteHistory.ResponsibleUser = request.ResponsibleUser;
            quoteHistory.UpdatedBy = request.UpdatedBy;
            quoteHistory.DtUpdated = DateTime.UtcNow;

            unitOfWork.QuoteHistoryRepository.Update(quoteHistory);

            return Task.FromResult<QuoteHistoryResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(quoteHistory));
        }
    }
}
