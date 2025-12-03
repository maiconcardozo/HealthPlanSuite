using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all age ranges.
    /// </summary>
    public class GetAllAgeRangesQuery : IRequest<IEnumerable<AgeRangeResponseDTO>>
    {
    }
}
