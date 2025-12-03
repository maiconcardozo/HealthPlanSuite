using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving all health plans.
    /// </summary>
    public class GetAllHealthPlansQueryHandler : IRequestHandler<GetAllHealthPlansQuery, IEnumerable<HealthPlanResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllHealthPlansQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<HealthPlanResponseDTO>> Handle(GetAllHealthPlansQuery request, CancellationToken cancellationToken)
        {
            var healthPlans = unitOfWork.HealthPlanRepository.GetAll().Where(hp => hp.IsActive);
            var healthPlanDtos = healthPlans.Select(hp => CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(hp));

            return Task.FromResult(healthPlanDtos);
        }
    }
}
