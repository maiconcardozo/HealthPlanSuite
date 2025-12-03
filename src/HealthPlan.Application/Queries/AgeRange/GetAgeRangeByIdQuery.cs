using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve an age range by ID.
    /// </summary>
    public class GetAgeRangeByIdQuery : IRequest<AgeRangeResponseDTO?>
    {
        public int Id { get; set; }
    }
}
