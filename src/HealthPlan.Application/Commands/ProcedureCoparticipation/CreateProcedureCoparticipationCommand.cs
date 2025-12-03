using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateProcedureCoparticipationCommand : IRequest<ProcedureCoparticipationResponseDTO>
    {
        public int HealthPlanId { get; set; }
        public string CoparticipationType { get; set; } = string.Empty;
        public string Procedure { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal? Limit { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
