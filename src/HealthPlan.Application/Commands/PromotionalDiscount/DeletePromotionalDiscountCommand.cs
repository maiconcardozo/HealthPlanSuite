using MediatR;

namespace HealthPlan.Application.Commands
{
    public class DeletePromotionalDiscountCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
