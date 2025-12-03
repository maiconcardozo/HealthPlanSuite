using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to update an existing quote.
    /// </summary>
    public class UpdateQuoteCommand : IRequest<QuoteResponseDTO?>
    {
        public int Id { get; set; }
        public int IdCompany { get; set; }
        public int IdBeneficiary { get; set; }
        public int IdHealthPlan { get; set; }
        public int IdAgeRange { get; set; }
        public decimal MonthlyPremium { get; set; }
        public DateTime ValidUntil { get; set; }
        public string? Notes { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
