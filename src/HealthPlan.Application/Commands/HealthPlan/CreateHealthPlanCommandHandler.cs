using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for creating new health plans.
    /// </summary>
    public class CreateHealthPlanCommandHandler : IRequestHandler<CreateHealthPlanCommand, HealthPlanResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateHealthPlanCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<HealthPlanResponseDTO> Handle(CreateHealthPlanCommand request, CancellationToken cancellationToken)
        {
            var healthPlan = new Domain.Entities.HealthPlan
            {
                CompanyId = request.CompanyId,
                Name = request.Name,
                Code = request.Code,
                Description = request.Description,
                PlanType = request.PlanType,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.HealthPlanRepository.Add(healthPlan);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(healthPlan));
        }
    }
}
