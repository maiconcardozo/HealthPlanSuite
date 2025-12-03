using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllPlanPriceRangesQuery : IRequest<IEnumerable<PlanPriceRangeResponseDTO>>
    {
    }
}
