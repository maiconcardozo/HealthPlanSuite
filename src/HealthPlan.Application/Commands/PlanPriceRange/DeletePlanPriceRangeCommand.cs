using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeletePlanPriceRangeCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
