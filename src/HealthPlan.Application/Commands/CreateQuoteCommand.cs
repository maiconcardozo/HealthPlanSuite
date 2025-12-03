using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to create a new quote.
    /// </summary>
    public class CreateQuoteCommand : IRequest<QuoteResponseDTO>
    {
        public int IdCompany { get; set; }
        public int IdBeneficiary { get; set; }
        public int IdHealthPlan { get; set; }
        public int IdAgeRange { get; set; }
        public decimal MonthlyPremium { get; set; }
        public DateTime ValidUntil { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
