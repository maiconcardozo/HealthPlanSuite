using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllAcceptanceRulesQuery : IRequest<IEnumerable<AcceptanceRuleResponseDTO>>
    {
    }
}
