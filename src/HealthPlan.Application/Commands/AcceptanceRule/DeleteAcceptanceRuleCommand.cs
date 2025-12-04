using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteAcceptanceRuleCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
