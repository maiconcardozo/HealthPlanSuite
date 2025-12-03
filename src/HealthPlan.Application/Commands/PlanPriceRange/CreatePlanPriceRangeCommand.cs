using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreatePlanPriceRangeCommand : IRequest<PlanPriceRangeResponseDTO>
    {
        public int HealthPlanId { get; set; }
        public int AgeRangeId { get; set; }
        public string ContractType { get; set; } = string.Empty;
        public string CoparticipationType { get; set; } = string.Empty;
        public decimal OriginalValue { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
