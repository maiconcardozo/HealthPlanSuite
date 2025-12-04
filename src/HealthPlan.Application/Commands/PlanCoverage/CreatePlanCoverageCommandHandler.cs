using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreatePlanCoverageCommandHandler : IRequestHandler<CreatePlanCoverageCommand, PlanCoverageResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreatePlanCoverageCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PlanCoverageResponseDTO> Handle(CreatePlanCoverageCommand request, CancellationToken cancellationToken)
        {
            var planCoverage = new PlanCoverage
            {
                IdHealthPlan = request.HealthPlanId,
                IdCoverage = request.CoverageId,
                PremiumValue = request.PremiumValue,
                IsIncluded = request.IsIncluded,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.PlanCoverageRepository.Add(planCoverage);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(planCoverage));
        }
    }
}
