using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to update an existing age range.
    /// </summary>
    public class UpdateAgeRangeCommand : IRequest<AgeRangeResponseDTO?>
    {
        public int Id { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }
}
