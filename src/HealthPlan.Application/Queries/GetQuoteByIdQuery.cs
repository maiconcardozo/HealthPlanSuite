using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve a quote by ID.
    /// </summary>
    public class GetQuoteByIdQuery : IRequest<QuoteResponseDTO?>
    {
        public int Id { get; set; }
    }
}
