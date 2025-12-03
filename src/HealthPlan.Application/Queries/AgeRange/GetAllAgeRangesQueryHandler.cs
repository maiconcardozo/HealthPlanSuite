using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving all age ranges.
    /// </summary>
    public class GetAllAgeRangesQueryHandler : IRequestHandler<GetAllAgeRangesQuery, IEnumerable<AgeRangeResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllAgeRangesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<AgeRangeResponseDTO>> Handle(GetAllAgeRangesQuery request, CancellationToken cancellationToken)
        {
            var ageRanges = unitOfWork.AgeRangeRepository.GetAll().Where(ar => ar.IsActive);
            var ageRangeDtos = ageRanges.Select(ar => CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ar));

            return Task.FromResult(ageRangeDtos);
        }
    }
}
