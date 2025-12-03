using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreatePlanPriceRangeCommandHandler : IRequestHandler<CreatePlanPriceRangeCommand, PlanPriceRangeResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreatePlanPriceRangeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PlanPriceRangeResponseDTO> Handle(CreatePlanPriceRangeCommand request, CancellationToken cancellationToken)
        {
            var planPriceRange = new PlanPriceRange
            {
                HealthPlanId = request.HealthPlanId,
                AgeRangeId = request.AgeRangeId,
                ContractType = request.ContractType,
                CoparticipationType = request.CoparticipationType,
                OriginalValue = request.OriginalValue,
                DiscountValue = request.DiscountValue,
                ValidityStart = request.ValidityStart,
                ValidityEnd = request.ValidityEnd,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.PlanPriceRangeRepository.Add(planPriceRange);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(planPriceRange));
        }
    }
}
