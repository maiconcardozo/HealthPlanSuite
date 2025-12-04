using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetPlanPriceRangeByIdQueryHandler : IRequestHandler<GetPlanPriceRangeByIdQuery, PlanPriceRangeResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetPlanPriceRangeByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<PlanPriceRangeResponseDTO?> Handle(GetPlanPriceRangeByIdQuery request, CancellationToken cancellationToken)
        {
            var planPriceRange = unitOfWork.PlanPriceRangeRepository.GetById(request.Id);

            if (planPriceRange == null)
            {
                return Task.FromResult<PlanPriceRangeResponseDTO?>(null);
            }

            var planPriceRangeDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(planPriceRange);
            return Task.FromResult<PlanPriceRangeResponseDTO?>(planPriceRangeDto);
        }
    }
}
