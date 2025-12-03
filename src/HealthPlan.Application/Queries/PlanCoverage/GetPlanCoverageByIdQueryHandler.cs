using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetPlanCoverageByIdQueryHandler : IRequestHandler<GetPlanCoverageByIdQuery, PlanCoverageResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetPlanCoverageByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PlanCoverageResponseDTO?> Handle(GetPlanCoverageByIdQuery request, CancellationToken cancellationToken)
        {
            var planCoverage = unitOfWork.PlanCoverageRepository.GetById(request.Id);

            if (planCoverage == null)
            {
                return Task.FromResult<PlanCoverageResponseDTO?>(null);
            }

            var planCoverageDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(planCoverage);
            return Task.FromResult<PlanCoverageResponseDTO?>(planCoverageDto);
        }
    }
}
