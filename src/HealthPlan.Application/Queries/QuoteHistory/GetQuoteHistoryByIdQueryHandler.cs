using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetQuoteHistoryByIdQueryHandler : IRequestHandler<GetQuoteHistoryByIdQuery, QuoteHistoryResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetQuoteHistoryByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<QuoteHistoryResponseDTO?> Handle(GetQuoteHistoryByIdQuery request, CancellationToken cancellationToken)
        {
            var quoteHistory = unitOfWork.QuoteHistoryRepository.GetById(request.Id);

            if (quoteHistory == null)
            {
                return Task.FromResult<QuoteHistoryResponseDTO?>(null);
            }

            var quoteHistoryDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(quoteHistory);
            return Task.FromResult<QuoteHistoryResponseDTO?>(quoteHistoryDto);
        }
    }
}
