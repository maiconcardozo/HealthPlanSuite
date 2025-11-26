using MediatR;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Application.Commands.Quote
{
    /// <summary>
    /// Handler for the CreateQuoteCommand.
    /// Creates a new quote in the system.
    /// </summary>
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, CreateQuoteResponse>
    {
        private readonly IQuoteService _quoteService;

        /// <summary>
        /// Initializes a new instance of the CreateQuoteCommandHandler class.
        /// </summary>
        /// <param name="quoteService">Quote service for business operations</param>
        public CreateQuoteCommandHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        /// <summary>
        /// Handles the CreateQuoteCommand.
        /// </summary>
        /// <param name="request">The command containing quote data</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response with the created quote details</returns>
        public Task<CreateQuoteResponse> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            var quote = new Domain.Implementation.Quote
            {
                IdCompany = request.IdCompany,
                IdBeneficiary = request.IdBeneficiary,
                IdHealthPlan = request.IdHealthPlan,
                IdAgeRange = request.IdAgeRange,
                MonthlyPremium = request.MonthlyPremium,
                ValidUntil = request.ValidUntil,
                CreatedBy = request.CreatedBy,
                Notes = request.Notes,
                Status = "Pending"
            };

            _quoteService.AddQuote(quote);

            var response = new CreateQuoteResponse
            {
                Id = quote.Id,
                QuoteNumber = quote.QuoteNumber,
                Status = quote.Status,
                MonthlyPremium = quote.MonthlyPremium,
                QuoteDate = quote.QuoteDate
            };

            return Task.FromResult(response);
        }
    }
}
