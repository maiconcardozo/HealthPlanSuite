using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all quotes.
    /// </summary>
    public class GetAllQuotesQuery : IRequest<IEnumerable<QuoteResponseDTO>>
    {
    }
}
