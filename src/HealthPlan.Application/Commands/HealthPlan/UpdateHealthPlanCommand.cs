using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to update an existing health plan.
    /// </summary>
    public class UpdateHealthPlanCommand : IRequest<HealthPlanResponseDTO?>
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string PlanType { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }
}
