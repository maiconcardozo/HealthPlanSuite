using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateAcceptanceRuleCommand : IRequest<AcceptanceRuleResponseDTO>
    {
        public int HealthPlanId { get; set; }
        public string RuleType { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public string? ValuesList { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? RejectionMessage { get; set; }
        public bool IsMandatory { get; set; } = true;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
