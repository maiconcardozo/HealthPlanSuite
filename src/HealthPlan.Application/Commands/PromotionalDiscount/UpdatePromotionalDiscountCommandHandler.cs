using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdatePromotionalDiscountCommandHandler : IRequestHandler<UpdatePromotionalDiscountCommand, PromotionalDiscountResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdatePromotionalDiscountCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PromotionalDiscountResponseDTO?> Handle(UpdatePromotionalDiscountCommand request, CancellationToken cancellationToken)
        {
            var promotionalDiscount = unitOfWork.PromotionalDiscountRepository.GetById(request.Id);

            if (promotionalDiscount == null)
            {
                return Task.FromResult<PromotionalDiscountResponseDTO?>(null);
            }

            promotionalDiscount.HealthPlanId = request.HealthPlanId;
            promotionalDiscount.DiscountPercentage = request.DiscountPercentage;
            promotionalDiscount.ValidityStart = request.ValidityStart;
            promotionalDiscount.ValidityEnd = request.ValidityEnd;
            promotionalDiscount.Observation = request.Observation;
            promotionalDiscount.UpdatedBy = request.UpdatedBy;
            promotionalDiscount.DtUpdated = DateTime.UtcNow;

            unitOfWork.PromotionalDiscountRepository.Update(promotionalDiscount);

            return Task.FromResult<PromotionalDiscountResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<PromotionalDiscountResponseDTO>(promotionalDiscount));
        }
    }
}
