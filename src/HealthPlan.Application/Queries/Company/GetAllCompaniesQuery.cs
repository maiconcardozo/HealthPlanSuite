using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve all companies.
    /// </summary>
    public class GetAllCompaniesQuery : IRequest<IEnumerable<CompanyResponseDTO>>
    {
    }
}
