using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating health plans.
    /// </summary>
    public class UpdateHealthPlanCommandHandler : IRequestHandler<UpdateHealthPlanCommand, HealthPlanResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateHealthPlanCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<HealthPlanResponseDTO?> Handle(UpdateHealthPlanCommand request, CancellationToken cancellationToken)
        {
            var healthPlan = unitOfWork.HealthPlanRepository.GetById(request.Id);

            if (healthPlan == null)
            {
                return Task.FromResult<HealthPlanResponseDTO?>(null);
            }

            healthPlan.CompanyId = request.CompanyId;
            healthPlan.Name = request.Name;
            healthPlan.Code = request.Code;
            healthPlan.Description = request.Description;
            healthPlan.PlanType = request.PlanType;
            healthPlan.UpdatedBy = request.UpdatedBy;
            healthPlan.DtUpdated = DateTime.UtcNow;

            unitOfWork.HealthPlanRepository.Update(healthPlan);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult<HealthPlanResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(healthPlan));
        }
    }
}
