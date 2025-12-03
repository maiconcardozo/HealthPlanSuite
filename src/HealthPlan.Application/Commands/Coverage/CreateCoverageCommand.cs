using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to create a new coverage.
    /// </summary>
    public class CreateCoverageCommand : IRequest<CoverageResponseDTO>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CoverageType { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
