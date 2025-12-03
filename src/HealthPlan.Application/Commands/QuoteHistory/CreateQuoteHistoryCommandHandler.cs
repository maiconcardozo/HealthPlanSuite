using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateQuoteHistoryCommandHandler : IRequestHandler<CreateQuoteHistoryCommand, QuoteHistoryResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateQuoteHistoryCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<QuoteHistoryResponseDTO> Handle(CreateQuoteHistoryCommand request, CancellationToken cancellationToken)
        {
            var quoteHistory = new QuoteHistory
            {
                IdQuote = request.QuoteId,
                PreviousStatus = request.PreviousStatus,
                NewStatus = request.NewStatus,
                Reason = request.Reason,
                Observations = request.Observations,
                ChangeDate = request.ChangeDate,
                ResponsibleUser = request.ResponsibleUser,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.QuoteHistoryRepository.Add(quoteHistory);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(quoteHistory));
        }
    }
}
