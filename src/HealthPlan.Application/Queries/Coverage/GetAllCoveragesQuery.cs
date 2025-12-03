using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all coverages.
    /// </summary>
    public class GetAllCoveragesQuery : IRequest<IEnumerable<CoverageResponseDTO>>
    {
    }
}
