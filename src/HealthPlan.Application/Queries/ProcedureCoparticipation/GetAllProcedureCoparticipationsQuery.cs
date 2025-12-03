using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllProcedureCoparticipationsQuery : IRequest<IEnumerable<ProcedureCoparticipationResponseDTO>>
    {
    }
}
