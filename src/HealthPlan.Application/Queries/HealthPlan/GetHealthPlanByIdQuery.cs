using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve a health plan by ID.
    /// </summary>
    public class GetHealthPlanByIdQuery : IRequest<HealthPlanResponseDTO?>
    {
        public int Id { get; set; }
    }
}
