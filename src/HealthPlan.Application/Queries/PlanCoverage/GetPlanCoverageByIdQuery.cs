using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetPlanCoverageByIdQuery : IRequest<PlanCoverageResponseDTO?>
    {
        public int Id { get; set; }
    }
}
