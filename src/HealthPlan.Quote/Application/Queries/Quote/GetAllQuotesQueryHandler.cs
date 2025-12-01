using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.UnitOfWork.Interface;
using MediatR;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Handler for retrieving all quotes.
    /// </summary>
    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, IEnumerable<QuoteResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllQuotesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<QuoteResponseDTO>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            var quotes = unitOfWork.QuoteRepository.GetAll();
            var quoteDtos = quotes.Select(q => CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(q));
            
            return Task.FromResult(quoteDtos);
        }
    }
}
