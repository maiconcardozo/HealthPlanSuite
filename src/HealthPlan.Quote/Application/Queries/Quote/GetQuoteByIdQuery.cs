using HealthPlan.Quote.DTO;
using MediatR;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Query to retrieve a quote by ID.
    /// </summary>
    public class GetQuoteByIdQuery : IRequest<QuoteResponseDTO?>
    {
        public int Id { get; set; }
    }
}
