using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Handler for retrieving an age range by ID.
    /// </summary>
    public class GetAgeRangeByIdQueryHandler : IRequestHandler<GetAgeRangeByIdQuery, AgeRangeResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAgeRangeByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AgeRangeResponseDTO?> Handle(GetAgeRangeByIdQuery request, CancellationToken cancellationToken)
        {
            var ageRange = unitOfWork.AgeRangeRepository.GetById(request.Id);

            if (ageRange == null)
            {
                return Task.FromResult<AgeRangeResponseDTO?>(null);
            }

            var ageRangeDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<AgeRangeResponseDTO>(ageRange);
            return Task.FromResult<AgeRangeResponseDTO?>(ageRangeDto);
        }
    }
}
