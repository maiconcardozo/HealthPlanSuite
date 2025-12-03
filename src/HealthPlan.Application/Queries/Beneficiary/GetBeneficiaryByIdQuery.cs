using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve a beneficiary by ID.
    /// </summary>
    public class GetBeneficiaryByIdQuery : IRequest<BeneficiaryResponseDTO?>
    {
        public int Id { get; set; }
    }
}
