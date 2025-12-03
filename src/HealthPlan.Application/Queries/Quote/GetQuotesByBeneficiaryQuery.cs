using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve quotes by beneficiary ID.
    /// </summary>
    public class GetQuotesByBeneficiaryQuery : IRequest<IEnumerable<QuoteResponseDTO>>
    {
        public int BeneficiaryId { get; set; }
    }
}
