using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving quotes by beneficiary ID.
    /// </summary>
    public class GetQuotesByBeneficiaryQueryHandler : IRequestHandler<GetQuotesByBeneficiaryQuery, IEnumerable<QuoteResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetQuotesByBeneficiaryQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<QuoteResponseDTO>> Handle(GetQuotesByBeneficiaryQuery request, CancellationToken cancellationToken)
        {
            var quotes = unitOfWork.QuoteRepository.GetByBeneficiaryId(request.BeneficiaryId);
            var quoteDtos = quotes.Select(q => CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(q));

            return Task.FromResult(quoteDtos);
        }
    }
}
