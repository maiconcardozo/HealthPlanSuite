using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetProcedureCoparticipationByIdQuery : IRequest<ProcedureCoparticipationResponseDTO?>
    {
        public int Id { get; set; }
    }
}
