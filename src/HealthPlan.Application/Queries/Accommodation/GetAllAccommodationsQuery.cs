using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all accommodations.
    /// </summary>
    public class GetAllAccommodationsQuery : IRequest<IEnumerable<AccommodationResponseDTO>>
    {
    }
}
