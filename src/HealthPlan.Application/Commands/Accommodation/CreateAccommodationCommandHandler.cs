using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for creating new accommodations.
    /// </summary>
    public class CreateAccommodationCommandHandler : IRequestHandler<CreateAccommodationCommand, AccommodationResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateAccommodationCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AccommodationResponseDTO> Handle(CreateAccommodationCommand request, CancellationToken cancellationToken)
        {
            var accommodation = new Accommodation
            {
                Type = request.Type,
                Description = request.Description,
                AdditionalValue = request.AdditionalValue,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.AccommodationRepository.Add(accommodation);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(accommodation));
        }
    }
}
