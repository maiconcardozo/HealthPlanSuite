using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllPromotionalDiscountsQueryHandler : IRequestHandler<GetAllPromotionalDiscountsQuery, IEnumerable<PromotionalDiscountResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllPromotionalDiscountsQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<PromotionalDiscountResponseDTO>> Handle(GetAllPromotionalDiscountsQuery request, CancellationToken cancellationToken)
        {
            var promotionalDiscounts = unitOfWork.PromotionalDiscountRepository.GetAll().Where(pd => pd.IsActive);
            var promotionalDiscountDtos = promotionalDiscounts.Select(pd => CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(pd));

            return Task.FromResult(promotionalDiscountDtos);
        }
    }
}
