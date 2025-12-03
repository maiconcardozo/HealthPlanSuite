using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdatePlanPriceRangeCommand : IRequest<PlanPriceRangeResponseDTO?>
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public int AgeRangeId { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public string CoparticipationType { get; set; } = string.Empty;
        public decimal OriginalValue { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
