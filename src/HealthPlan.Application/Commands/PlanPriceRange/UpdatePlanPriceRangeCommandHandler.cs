using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdatePlanPriceRangeCommandHandler : IRequestHandler<UpdatePlanPriceRangeCommand, PlanPriceRangeResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdatePlanPriceRangeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PlanPriceRangeResponseDTO?> Handle(UpdatePlanPriceRangeCommand request, CancellationToken cancellationToken)
        {
            var planPriceRange = unitOfWork.PlanPriceRangeRepository.GetById(request.Id);

            if (planPriceRange == null)
            {
                return Task.FromResult<PlanPriceRangeResponseDTO?>(null);
            }

            planPriceRange.HealthPlanId = request.HealthPlanId;
            planPriceRange.AgeRangeId = request.AgeRangeId;
            planPriceRange.ContractType = request.ContractType;
            planPriceRange.CoparticipationType = request.CoparticipationType;
            planPriceRange.OriginalValue = request.OriginalValue;
            planPriceRange.DiscountValue = request.DiscountValue;
            planPriceRange.ValidityStart = request.ValidityStart;
            planPriceRange.ValidityEnd = request.ValidityEnd;
            planPriceRange.UpdatedBy = request.UpdatedBy;
            planPriceRange.DtUpdated = DateTime.UtcNow;

            unitOfWork.PlanPriceRangeRepository.Update(planPriceRange);

            return Task.FromResult<PlanPriceRangeResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(planPriceRange));
        }
    }
}
