using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetPlanPriceRangeByIdQuery : IRequest<PlanPriceRangeResponseDTO?>
    {
        public int Id { get; set; }
    }
}
