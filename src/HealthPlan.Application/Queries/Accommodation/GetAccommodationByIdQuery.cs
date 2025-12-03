using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve an accommodation by ID.
    /// </summary>
    public class GetAccommodationByIdQuery : IRequest<AccommodationResponseDTO?>
    {
        public int Id { get; set; }
    }
}
