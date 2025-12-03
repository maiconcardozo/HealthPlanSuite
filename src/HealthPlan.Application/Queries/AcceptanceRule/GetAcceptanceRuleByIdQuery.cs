using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAcceptanceRuleByIdQuery : IRequest<AcceptanceRuleResponseDTO?>
    {
        public int Id { get; set; }
    }
}
