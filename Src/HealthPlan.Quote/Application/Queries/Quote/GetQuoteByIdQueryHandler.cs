using HealthPlan.Quote.DTO;
using HealthPlan.Quote.Mapping;
using HealthPlan.Quote.UnitOfWork.Interface;
using MediatR;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Handler for retrieving a quote by ID.
    /// </summary>
    public class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery, QuoteResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetQuoteByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<QuoteResponseDTO?> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
        {
            var quote = unitOfWork.QuoteRepository.GetById(request.Id);

            if (quote == null)
            {
                return Task.FromResult<QuoteResponseDTO?>(null);
            }

            var quoteDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<QuoteResponseDTO>(quote);
            return Task.FromResult<QuoteResponseDTO?>(quoteDto);
        }
    }
}
