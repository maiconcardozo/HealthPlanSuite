using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreatePromotionalDiscountCommand : IRequest<PromotionalDiscountResponseDTO>
    {
        public int HealthPlanId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string? Observation { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
