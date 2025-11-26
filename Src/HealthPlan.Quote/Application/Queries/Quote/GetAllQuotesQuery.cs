using MediatR;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Query to get all active quotes.
    /// </summary>
    public class GetAllQuotesQuery : IRequest<IEnumerable<GetQuoteByIdResponse>>
    {
    }

    /// <summary>
    /// Handler for the GetAllQuotesQuery.
    /// Retrieves all active quotes from the system.
    /// </summary>
    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, IEnumerable<GetQuoteByIdResponse>>
    {
        private readonly Services.Interface.IQuoteService _quoteService;

        /// <summary>
        /// Initializes a new instance of the GetAllQuotesQueryHandler class.
        /// </summary>
        /// <param name="quoteService">Quote service for data retrieval</param>
        public GetAllQuotesQueryHandler(Services.Interface.IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        /// <summary>
        /// Handles the GetAllQuotesQuery.
        /// </summary>
        /// <param name="request">The query</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Collection of all active quotes</returns>
        public Task<IEnumerable<GetQuoteByIdResponse>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            var quotes = _quoteService.GetAllActiveQuotes();

            var response = quotes.Select(quote => new GetQuoteByIdResponse
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
            });

            return Task.FromResult(response);
        }
    }
}
