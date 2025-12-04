using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdatePlanCoverageCommand : IRequest<PlanCoverageResponseDTO?>
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public int CoverageId { get; set; }
        public decimal PremiumValue { get; set; }
        public bool IsIncluded { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
