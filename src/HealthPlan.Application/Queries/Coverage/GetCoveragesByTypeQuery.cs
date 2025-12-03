using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve coverages by type.
    /// </summary>
    public class GetCoveragesByTypeQuery : IRequest<IEnumerable<CoverageResponseDTO>>
    {
        public string CoverageType { get; set; } = string.Empty;
    }
}
