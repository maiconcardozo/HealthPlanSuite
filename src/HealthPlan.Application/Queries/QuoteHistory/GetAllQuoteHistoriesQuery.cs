using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllQuoteHistoriesQuery : IRequest<IEnumerable<QuoteHistoryResponseDTO>>
    {
    }
}
