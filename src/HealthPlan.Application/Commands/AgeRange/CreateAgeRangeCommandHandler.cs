using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Handler for creating new age ranges.
    /// </summary>
    public class CreateAgeRangeCommandHandler : IRequestHandler<CreateAgeRangeCommand, AgeRangeResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateAgeRangeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AgeRangeResponseDTO> Handle(CreateAgeRangeCommand request, CancellationToken cancellationToken)
        {
            var ageRange = new AgeRange
            {
                MinAge = request.MinAge,
                MaxAge = request.MaxAge,
                Description = request.Description,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.AgeRangeRepository.Add(ageRange);

            // Transaction will be committed by TransactionBehavior

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ageRange));
        }
    }
}
