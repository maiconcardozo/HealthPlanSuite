using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving an accommodation by ID.
    /// </summary>
    public class GetAccommodationByIdQueryHandler : IRequestHandler<GetAccommodationByIdQuery, AccommodationResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAccommodationByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AccommodationResponseDTO?> Handle(GetAccommodationByIdQuery request, CancellationToken cancellationToken)
        {
            var accommodation = unitOfWork.AccommodationRepository.GetById(request.Id);

            if (accommodation == null)
            {
                return Task.FromResult<AccommodationResponseDTO?>(null);
            }

            var accommodationDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(accommodation);
            return Task.FromResult<AccommodationResponseDTO?>(accommodationDto);
        }
    }
}
