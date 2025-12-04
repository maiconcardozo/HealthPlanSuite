using HealthPlan.Application.DTOs;
using HealthPlan.Application.Mappers;
using HealthPlan.Domain.Interfaces;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllProcedureCoparticipationsQueryHandler : IRequestHandler<GetAllProcedureCoparticipationsQuery, IEnumerable<ProcedureCoparticipationResponseDTO>>
    {
        private readonly IApplicationUnitOfWork unitOfWork;

        public GetAllProcedureCoparticipationsQueryHandler(IApplicationUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<ProcedureCoparticipationResponseDTO>> Handle(GetAllProcedureCoparticipationsQuery request, CancellationToken cancellationToken)
        {
            var procedureCoparticipations = unitOfWork.ProcedureCoparticipationRepository.GetAll().Where(pc => pc.IsActive);
            var procedureCoparticipationDtos = procedureCoparticipations.Select(pc => CleanTemplateApplicationMapperInitializer.Mapper.Map<ProcedureCoparticipationResponseDTO>(pc));

            return Task.FromResult(procedureCoparticipationDtos);
        }
    }
}
