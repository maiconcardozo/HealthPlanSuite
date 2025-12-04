using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetPromotionalDiscountByIdQuery : IRequest<PromotionalDiscountResponseDTO?>
    {
        public int Id { get; set; }
    }
}
