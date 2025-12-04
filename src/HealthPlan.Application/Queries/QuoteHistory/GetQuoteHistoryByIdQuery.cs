using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetQuoteHistoryByIdQuery : IRequest<QuoteHistoryResponseDTO?>
    {
        public int Id { get; set; }
    }
}
