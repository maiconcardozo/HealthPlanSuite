using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve a coverage by ID.
    /// </summary>
    public class GetCoverageByIdQuery : IRequest<CoverageResponseDTO?>
    {
        public int Id { get; set; }
    }
}
