using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreatePlanCoverageCommand : IRequest<PlanCoverageResponseDTO>
    {
        public int HealthPlanId { get; set; }
        public int CoverageId { get; set; }
        public decimal PremiumValue { get; set; }
        public bool IsIncluded { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
