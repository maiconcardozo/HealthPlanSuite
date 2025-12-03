using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to delete a health plan.
    /// </summary>
    public class DeleteHealthPlanCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
