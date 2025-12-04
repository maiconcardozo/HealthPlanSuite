using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateProcedureCoparticipationCommandHandler : IRequestHandler<CreateProcedureCoparticipationCommand, ProcedureCoparticipationResponseDTO>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public CreateProcedureCoparticipationCommandHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ProcedureCoparticipationResponseDTO> Handle(CreateProcedureCoparticipationCommand request, CancellationToken cancellationToken)
        {
            var procedureCoparticipation = new ProcedureCoparticipation
            {
                HealthPlanId = request.HealthPlanId,
                CoparticipationType = request.CoparticipationType,
                Procedure = request.Procedure,
                Value = request.Value,
                Limit = request.Limit,
                CreatedBy = request.CreatedBy,
                DtCreated = DateTime.UtcNow,
            };

            unitOfWork.ProcedureCoparticipationRepository.Add(procedureCoparticipation);

            return Task.FromResult(CleanTemplateApplicationMapperInitializer.Mapper.Map<ProcedureCoparticipationResponseDTO>(procedureCoparticipation));
        }
    }
}
