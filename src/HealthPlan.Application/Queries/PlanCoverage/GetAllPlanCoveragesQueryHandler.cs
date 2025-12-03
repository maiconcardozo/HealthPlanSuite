using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllPlanCoveragesQueryHandler : IRequestHandler<GetAllPlanCoveragesQuery, IEnumerable<PlanCoverageResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllPlanCoveragesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<PlanCoverageResponseDTO>> Handle(GetAllPlanCoveragesQuery request, CancellationToken cancellationToken)
        {
            var planCoverages = unitOfWork.PlanCoverageRepository.GetAll().Where(pc => pc.IsActive);
            var planCoverageDtos = planCoverages.Select(pc => CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanCoverageResponseDTO>(pc));

            return Task.FromResult(planCoverageDtos);
        }
    }
}
