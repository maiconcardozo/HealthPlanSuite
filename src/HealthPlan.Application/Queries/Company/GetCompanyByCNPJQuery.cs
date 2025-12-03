using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Queries
{
    /// <summary>
    /// Query to retrieve a company by CNPJ.
    /// </summary>
    public class GetCompanyByCNPJQuery : IRequest<CompanyResponseDTO?>
    {
        public string CNPJ { get; set; } = string.Empty;
    }
}
