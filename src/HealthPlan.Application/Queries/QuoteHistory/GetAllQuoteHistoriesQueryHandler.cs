using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllQuoteHistoriesQueryHandler : IRequestHandler<GetAllQuoteHistoriesQuery, IEnumerable<QuoteHistoryResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllQuoteHistoriesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<QuoteHistoryResponseDTO>> Handle(GetAllQuoteHistoriesQuery request, CancellationToken cancellationToken)
        {
            var quoteHistories = unitOfWork.QuoteHistoryRepository.GetAll().Where(qh => qh.IsActive);
            var quoteHistoryDtos = quoteHistories.Select(qh => CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteHistoryResponseDTO>(qh));

            return Task.FromResult(quoteHistoryDtos);
        }
    }
}
