using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateAdhesionFeeCommandHandler : IRequestHandler<UpdateAdhesionFeeCommand, AdhesionFeeResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateAdhesionFeeCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<AdhesionFeeResponseDTO?> Handle(UpdateAdhesionFeeCommand request, CancellationToken cancellationToken)
        {
            var adhesionFee = unitOfWork.AdhesionFeeRepository.GetById(request.Id);

            if (adhesionFee == null)
            {
                return Task.FromResult<AdhesionFeeResponseDTO?>(null);
            }

            adhesionFee.HealthPlanId = request.HealthPlanId;
            adhesionFee.Value = request.Value;
            adhesionFee.ValidityStart = request.ValidityStart;
            adhesionFee.ValidityEnd = request.ValidityEnd;
            adhesionFee.UpdatedBy = request.UpdatedBy;
            adhesionFee.DtUpdated = DateTime.UtcNow;

            unitOfWork.AdhesionFeeRepository.Update(adhesionFee);

            return Task.FromResult<AdhesionFeeResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<AdhesionFeeResponseDTO>(adhesionFee));
        }
    }
}
