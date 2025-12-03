using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAdhesionFeeByIdQueryHandler : IRequestHandler<GetAdhesionFeeByIdQuery, AdhesionFeeResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAdhesionFeeByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AdhesionFeeResponseDTO?> Handle(GetAdhesionFeeByIdQuery request, CancellationToken cancellationToken)
        {
            var adhesionFee = unitOfWork.AdhesionFeeRepository.GetById(request.Id);

            if (adhesionFee == null)
            {
                return Task.FromResult<AdhesionFeeResponseDTO?>(null);
            }

            var adhesionFeeDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(adhesionFee);
            return Task.FromResult<AdhesionFeeResponseDTO?>(adhesionFeeDto);
        }
    }
}
