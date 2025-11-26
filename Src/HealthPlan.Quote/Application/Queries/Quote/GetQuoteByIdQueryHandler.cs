using MediatR;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Handler for the GetQuoteByIdQuery.
    /// Retrieves a quote by its ID.
    /// </summary>
    public class GetQuoteByIdQueryHandler : IRequestHandler<GetQuoteByIdQuery, GetQuoteByIdResponse?>
    {
        private readonly IQuoteService _quoteService;

        /// <summary>
        /// Initializes a new instance of the GetQuoteByIdQueryHandler class.
        /// </summary>
        /// <param name="quoteService">Quote service for data retrieval</param>
        public GetQuoteByIdQueryHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        /// <summary>
        /// Handles the GetQuoteByIdQuery.
        /// </summary>
        /// <param name="request">The query containing the quote ID</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The quote details if found, null otherwise</returns>
        public Task<GetQuoteByIdResponse?> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
        {
            var quote = _quoteService.GetById(request.Id);

            if (quote == null)
            {
                return Task.FromResult<GetQuoteByIdResponse?>(null);
            }

            var response = new GetQuoteByIdResponse
            {
                Id = quote.Id,
                QuoteNumber = quote.QuoteNumber,
                IdCompany = quote.IdCompany,
                IdBeneficiary = quote.IdBeneficiary,
                IdHealthPlan = quote.IdHealthPlan,
                IdAgeRange = quote.IdAgeRange,
                MonthlyPremium = quote.MonthlyPremium,
                Status = quote.Status,
                ValidUntil = quote.ValidUntil,
                QuoteDate = quote.QuoteDate,
                CreatedBy = quote.CreatedBy,
                Notes = quote.Notes
            };

            return Task.FromResult<GetQuoteByIdResponse?>(response);
        }
    }
}
