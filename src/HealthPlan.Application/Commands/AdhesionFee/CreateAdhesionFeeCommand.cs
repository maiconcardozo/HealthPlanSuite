using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    public class CreateAdhesionFeeCommand : IRequest<AdhesionFeeResponseDTO>
    {
        public int HealthPlanId { get; set; }
        public decimal Value { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
