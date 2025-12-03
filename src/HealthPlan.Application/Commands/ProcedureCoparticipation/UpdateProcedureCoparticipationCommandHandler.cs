using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateProcedureCoparticipationCommandHandler : IRequestHandler<UpdateProcedureCoparticipationCommand, ProcedureCoparticipationResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public UpdateProcedureCoparticipationCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ProcedureCoparticipationResponseDTO?> Handle(UpdateProcedureCoparticipationCommand request, CancellationToken cancellationToken)
        {
            var procedureCoparticipation = unitOfWork.ProcedureCoparticipationRepository.GetById(request.Id);

            if (procedureCoparticipation == null)
            {
                return Task.FromResult<ProcedureCoparticipationResponseDTO?>(null);
            }

            procedureCoparticipation.HealthPlanId = request.HealthPlanId;
            procedureCoparticipation.CoparticipationType = request.CoparticipationType;
            procedureCoparticipation.Procedure = request.Procedure;
            procedureCoparticipation.Value = request.Value;
            procedureCoparticipation.Limit = request.Limit;
            procedureCoparticipation.UpdatedBy = request.UpdatedBy;
            procedureCoparticipation.DtUpdated = DateTime.UtcNow;

            unitOfWork.ProcedureCoparticipationRepository.Update(procedureCoparticipation);

            return Task.FromResult<ProcedureCoparticipationResponseDTO?>(CleanTemplateApplicationMapperInitializer.Mapper.Map<ProcedureCoparticipationResponseDTO>(procedureCoparticipation));
        }
    }
}
