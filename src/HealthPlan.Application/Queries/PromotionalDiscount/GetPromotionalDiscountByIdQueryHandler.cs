using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetPromotionalDiscountByIdQueryHandler : IRequestHandler<GetPromotionalDiscountByIdQuery, PromotionalDiscountResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetPromotionalDiscountByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PromotionalDiscountResponseDTO?> Handle(GetPromotionalDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            var promotionalDiscount = unitOfWork.PromotionalDiscountRepository.GetById(request.Id);

            if (promotionalDiscount == null)
            {
                return Task.FromResult<PromotionalDiscountResponseDTO?>(null);
            }

            var promotionalDiscountDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(promotionalDiscount);
            return Task.FromResult<PromotionalDiscountResponseDTO?>(promotionalDiscountDto);
        }
    }
}
