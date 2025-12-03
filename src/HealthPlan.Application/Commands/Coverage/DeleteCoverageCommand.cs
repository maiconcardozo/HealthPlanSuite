using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete a coverage.
    /// </summary>
    public class DeleteCoverageCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
