using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class UpdateProcedureCoparticipationCommand : IRequest<ProcedureCoparticipationResponseDTO?>
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public string CoparticipationType { get; set; } = string.Empty;
        public string Procedure { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public decimal? Limit { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
