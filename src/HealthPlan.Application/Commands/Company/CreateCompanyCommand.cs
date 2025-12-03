using HealthPlan.Application.DTOs;
using MediatR;

namespace HealthPlan.Application.Commands
{
    /// <summary>
    /// Command to create a new company.
    /// </summary>
    public class CreateCompanyCommand : IRequest<CompanyResponseDTO>
    {
        public string Name { get; set; } = string.Empty;
        public string? TradeName { get; set; }
        public string CNPJ { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }
}
