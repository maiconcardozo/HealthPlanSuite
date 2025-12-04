using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllPlanPriceRangesQueryHandler : IRequestHandler<GetAllPlanPriceRangesQuery, IEnumerable<PlanPriceRangeResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllPlanPriceRangesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<PlanPriceRangeResponseDTO>> Handle(GetAllPlanPriceRangesQuery request, CancellationToken cancellationToken)
        {
            var planPriceRanges = unitOfWork.PlanPriceRangeRepository.GetAll().Where(ppr => ppr.IsActive);
            var planPriceRangeDtos = planPriceRanges.Select(ppr => CleanTemplateApplicationMapperInitializer.Mapper.Map<PlanPriceRangeResponseDTO>(ppr));

            return Task.FromResult(planPriceRangeDtos);
        }
    }
}
