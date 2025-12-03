using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllPlanCoveragesQuery : IRequest<IEnumerable<PlanCoverageResponseDTO>>
    {
    }
}
