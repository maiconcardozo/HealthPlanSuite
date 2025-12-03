using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to create a new health plan.
    /// </summary>
    public class CreateHealthPlanCommand : IRequest<HealthPlanResponseDTO>
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string PlanType { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
