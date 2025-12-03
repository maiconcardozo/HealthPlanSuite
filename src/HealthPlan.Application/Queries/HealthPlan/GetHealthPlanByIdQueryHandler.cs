using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving a health plan by ID.
    /// </summary>
    public class GetHealthPlanByIdQueryHandler : IRequestHandler<GetHealthPlanByIdQuery, HealthPlanResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetHealthPlanByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<HealthPlanResponseDTO?> Handle(GetHealthPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var healthPlan = unitOfWork.HealthPlanRepository.GetById(request.Id);

            if (healthPlan == null)
            {
                return Task.FromResult<HealthPlanResponseDTO?>(null);
            }

            var healthPlanDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<HealthPlanResponseDTO>(healthPlan);
            return Task.FromResult<HealthPlanResponseDTO?>(healthPlanDto);
        }
    }
}
