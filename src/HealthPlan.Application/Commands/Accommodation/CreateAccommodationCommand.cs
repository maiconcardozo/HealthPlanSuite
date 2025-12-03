using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to create a new accommodation.
    /// </summary>
    public class CreateAccommodationCommand : IRequest<AccommodationResponseDTO>
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal AdditionalValue { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
