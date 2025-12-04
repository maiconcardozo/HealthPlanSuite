using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAdhesionFeeByIdQuery : IRequest<AdhesionFeeResponseDTO?>
    {
        public int Id { get; set; }
    }
}
