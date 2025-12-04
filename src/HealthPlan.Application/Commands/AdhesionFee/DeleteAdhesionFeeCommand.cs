using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeleteAdhesionFeeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
