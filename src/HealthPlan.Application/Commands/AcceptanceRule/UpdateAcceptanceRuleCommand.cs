using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateAcceptanceRuleCommand : IRequest<AcceptanceRuleResponseDTO?>
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public string RuleType { get; set; } = string.Empty;
        public string Operator { get; set; } = string.Empty;
        public string? MinValue { get; set; }
        public string? MaxValue { get; set; }
        public string? ValuesList { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? RejectionMessage { get; set; }
        public bool IsMandatory { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
