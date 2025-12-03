using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetProcedureCoparticipationByIdQueryHandler : IRequestHandler<GetProcedureCoparticipationByIdQuery, ProcedureCoparticipationResponseDTO?>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetProcedureCoparticipationByIdQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<ProcedureCoparticipationResponseDTO?> Handle(GetProcedureCoparticipationByIdQuery request, CancellationToken cancellationToken)
        {
            var procedureCoparticipation = unitOfWork.ProcedureCoparticipationRepository.GetById(request.Id);

            if (procedureCoparticipation == null)
            {
                return Task.FromResult<ProcedureCoparticipationResponseDTO?>(null);
            }

            var procedureCoparticipationDto = CleanTemplateApplicationMapperInitializer.Mapper.Map<ProcedureCoparticipationResponseDTO>(procedureCoparticipation);
            return Task.FromResult<ProcedureCoparticipationResponseDTO?>(procedureCoparticipationDto);
        }
    }
}
