using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for updating age ranges.
    /// </summary>
    public class UpdateAgeRangeCommandHandler : IRequestHandler<UpdateAgeRangeCommand, AgeRangeResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateAgeRangeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AgeRangeResponseDTO?> Handle(UpdateAgeRangeCommand request, CancellationToken cancellationToken)
        {
            var ageRange = unitOfWork.AgeRangeRepository.GetById(request.Id);

            if (ageRange == null)
            {
                return Task.FromResult<AgeRangeResponseDTO?>(null);
            }

            ageRange.MinAge = request.MinAge;
            ageRange.MaxAge = request.MaxAge;
            ageRange.Description = request.Description;
            ageRange.UpdatedBy = request.UpdatedBy;
            ageRange.DtUpdated = DateTime.UtcNow;

            unitOfWork.AgeRangeRepository.Update(ageRange);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult<AgeRangeResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ageRange));
        }
    }
}
