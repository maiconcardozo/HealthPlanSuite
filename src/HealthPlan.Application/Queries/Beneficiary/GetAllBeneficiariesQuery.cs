using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all beneficiaries.
    /// </summary>
    public class GetAllBeneficiariesQuery : IRequest<IEnumerable<BeneficiaryResponseDTO>>
    {
    }
}
