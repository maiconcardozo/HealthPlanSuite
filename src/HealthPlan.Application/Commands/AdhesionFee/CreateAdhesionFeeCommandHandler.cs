using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateAdhesionFeeCommandHandler : IRequestHandler<CreateAdhesionFeeCommand, AdhesionFeeResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateAdhesionFeeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AdhesionFeeResponseDTO> Handle(CreateAdhesionFeeCommand request, CancellationToken cancellationToken)
        {
            var adhesionFee = new AdhesionFee
            {
                HealthPlanId = request.HealthPlanId,
                Value = request.Value,
                ValidityStart = request.ValidityStart,
                ValidityEnd = request.ValidityEnd,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.AdhesionFeeRepository.Add(adhesionFee);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(adhesionFee));
        }
    }
}
