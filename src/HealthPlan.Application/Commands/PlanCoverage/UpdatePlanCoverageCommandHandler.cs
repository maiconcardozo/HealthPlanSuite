using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdatePlanCoverageCommandHandler : IRequestHandler<UpdatePlanCoverageCommand, PlanCoverageResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdatePlanCoverageCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PlanCoverageResponseDTO?> Handle(UpdatePlanCoverageCommand request, CancellationToken cancellationToken)
        {
            var planCoverage = unitOfWork.PlanCoverageRepository.GetById(request.Id);

            if (planCoverage == null)
            {
                return Task.FromResult<PlanCoverageResponseDTO?>(null);
            }

            planCoverage.IdHealthPlan = request.HealthPlanId;
            planCoverage.IdCoverage = request.CoverageId;
            planCoverage.PremiumValue = request.PremiumValue;
            planCoverage.IsIncluded = request.IsIncluded;
            planCoverage.UpdatedBy = request.UpdatedBy;
            planCoverage.DtUpdated = DateTime.UtcNow;

            unitOfWork.PlanCoverageRepository.Update(planCoverage);

            return Task.FromResult<PlanCoverageResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(planCoverage));
        }
    }
}
