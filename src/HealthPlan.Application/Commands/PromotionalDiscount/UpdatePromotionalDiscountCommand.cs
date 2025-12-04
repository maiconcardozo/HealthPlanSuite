using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdatePromotionalDiscountCommand : IRequest<PromotionalDiscountResponseDTO?>
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string? Observation { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
