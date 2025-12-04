using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllAdhesionFeesQueryHandler : IRequestHandler<GetAllAdhesionFeesQuery, IEnumerable<AdhesionFeeResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllAdhesionFeesQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<AdhesionFeeResponseDTO>> Handle(GetAllAdhesionFeesQuery request, CancellationToken cancellationToken)
        {
            var adhesionFees = unitOfWork.AdhesionFeeRepository.GetAll().Where(af => af.IsActive);
            var adhesionFeeDtos = adhesionFees.Select(af => CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(af));

            return Task.FromResult(adhesionFeeDtos);
        }
    }
}
