using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateQuoteHistoryCommand : IRequest<QuoteHistoryResponseDTO?>
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public string? PreviousStatus { get; set; }
        public string NewStatus { get; set; } = string.Empty;
        public string? Reason { get; set; }
        public string? Observations { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ResponsibleUser { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }
}
