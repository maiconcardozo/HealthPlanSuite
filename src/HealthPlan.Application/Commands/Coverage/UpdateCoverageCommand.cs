using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to update an existing coverage.
    /// </summary>
    public class UpdateCoverageCommand : IRequest<CoverageResponseDTO?>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string CoverageType { get; set; } = string.Empty;
        public string? UpdatedBy { get; set; }
    }
}
