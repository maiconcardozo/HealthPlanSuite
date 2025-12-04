using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to update an existing accommodation.
    /// </summary>
    public class UpdateAccommodationCommand : IRequest<AccommodationResponseDTO?>
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal AdditionalValue { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
