using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to create a new age range.
    /// </summary>
    public class CreateAgeRangeCommand : IRequest<AgeRangeResponseDTO>
    {
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
