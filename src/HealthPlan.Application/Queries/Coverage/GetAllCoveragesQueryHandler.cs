using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving all coverages.
    /// </summary>
    public class GetAllCoveragesQueryHandler : IRequestHandler<GetAllCoveragesQuery, IEnumerable<CoverageResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllCoveragesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<CoverageResponseDTO>> Handle(GetAllCoveragesQuery request, CancellationToken cancellationToken)
        {
            var coverages = unitOfWork.CoverageRepository.GetAll().Where(c => c.IsActive);
            var coverageDtos = coverages.Select(c => CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(c));

            return Task.FromResult(coverageDtos);
        }
    }
}
