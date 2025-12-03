using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving a coverage by ID.
    /// </summary>
    public class GetCoverageByIdQueryHandler : IRequestHandler<GetCoverageByIdQuery, CoverageResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetCoverageByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<CoverageResponseDTO?> Handle(GetCoverageByIdQuery request, CancellationToken cancellationToken)
        {
            var coverage = unitOfWork.CoverageRepository.GetById(request.Id);

            if (coverage == null)
            {
                return Task.FromResult<CoverageResponseDTO?>(null);
            }

            var coverageDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<CoverageResponseDTO>(coverage);
            return Task.FromResult<CoverageResponseDTO?>(coverageDto);
        }
    }
}
