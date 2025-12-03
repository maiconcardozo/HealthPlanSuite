using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving coverages by type.
    /// </summary>
    public class GetCoveragesByTypeQueryHandler : IRequestHandler<GetCoveragesByTypeQuery, IEnumerable<CoverageResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetCoveragesByTypeQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CoverageResponseDTO>> Handle(GetCoveragesByTypeQuery request, CancellationToken cancellationToken)
        {
            var coverages = unitOfWork.CoverageRepository.GetByCoverageType(request.CoverageType);
            var coverageDtos = coverages.Select(c => CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(c));

            return Task.FromResult(coverageDtos);
        }
    }
}
