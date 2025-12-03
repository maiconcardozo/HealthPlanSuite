using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve a company by ID.
    /// </summary>
    public class GetCompanyByIdQuery : IRequest<CompanyResponseDTO?>
    {
        public int Id { get; set; }
    }
}
