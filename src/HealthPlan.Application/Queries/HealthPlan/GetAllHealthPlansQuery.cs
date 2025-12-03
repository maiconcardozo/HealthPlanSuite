using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all health plans.
    /// </summary>
    public class GetAllHealthPlansQuery : IRequest<IEnumerable<HealthPlanResponseDTO>>
    {
    }
}
