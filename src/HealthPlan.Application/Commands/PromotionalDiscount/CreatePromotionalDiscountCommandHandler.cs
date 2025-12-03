using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreatePromotionalDiscountCommandHandler : IRequestHandler<CreatePromotionalDiscountCommand, PromotionalDiscountResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreatePromotionalDiscountCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PromotionalDiscountResponseDTO> Handle(CreatePromotionalDiscountCommand request, CancellationToken cancellationToken)
        {
            var promotionalDiscount = new PromotionalDiscount
            {
                HealthPlanId = request.HealthPlanId,
                DiscountPercentage = request.DiscountPercentage,
                ValidityStart = request.ValidityStart,
                ValidityEnd = request.ValidityEnd,
                Observation = request.Observation,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.PromotionalDiscountRepository.Add(promotionalDiscount);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(promotionalDiscount));
        }
    }
}
