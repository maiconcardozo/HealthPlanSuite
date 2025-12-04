using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeletePlanCoverageCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
