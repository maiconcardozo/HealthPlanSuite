using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating accommodations.
    /// </summary>
    public class UpdateAccommodationCommandHandler : IRequestHandler<UpdateAccommodationCommand, AccommodationResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateAccommodationCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AccommodationResponseDTO?> Handle(UpdateAccommodationCommand request, CancellationToken cancellationToken)
        {
            var accommodation = unitOfWork.AccommodationRepository.GetById(request.Id);

            if (accommodation == null)
            {
                return Task.FromResult<AccommodationResponseDTO?>(null);
            }

            accommodation.Type = request.Type;
            accommodation.Description = request.Description;
            accommodation.AdditionalValue = request.AdditionalValue;
            accommodation.UpdatedBy = request.UpdatedBy;
            accommodation.DtUpdated = DateTime.UtcNow;

            unitOfWork.AccommodationRepository.Update(accommodation);

            return Task.FromResult<AccommodationResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<AccommodationResponseDTO>(accommodation));
        }
    }
}
