using HealthPlan.Quote.DTO;
using MediatR;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Query to retrieve all quotes.
    /// </summary>
    public class GetAllQuotesQuery : IRequest<IEnumerable<QuoteResponseDTO>>
    {
    }
}
