using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    public class GetAllAdhesionFeesQuery : IRequest<IEnumerable<AdhesionFeeResponseDTO>>
    {
    }
}
