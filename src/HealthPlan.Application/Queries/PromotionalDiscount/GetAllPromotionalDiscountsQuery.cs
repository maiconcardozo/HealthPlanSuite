using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllPromotionalDiscountsQuery : IRequest<IEnumerable<PromotionalDiscountResponseDTO>>
    {
    }
}
