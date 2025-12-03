using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving all accommodations.
    /// </summary>
    public class GetAllAccommodationsQueryHandler : IRequestHandler<GetAllAccommodationsQuery, IEnumerable<AccommodationResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllAccommodationsQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<AccommodationResponseDTO>> Handle(GetAllAccommodationsQuery request, CancellationToken cancellationToken)
        {
            var accommodations = unitOfWork.AccommodationRepository.GetAll().Where(a => a.IsActive);
            var accommodationDtos = accommodations.Select(a => CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(a));

            return Task.FromResult(accommodationDtos);
        }
    }
}
