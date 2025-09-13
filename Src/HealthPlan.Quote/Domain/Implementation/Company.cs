using HealthPlan.Quote.Domain.Base;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a company implementation that offers health plans.
    /// Companies are the entities that provide health insurance services to beneficiaries.
    /// Inherits from Entity base class providing audit fields and implements ICompany interface.
    /// </summary>
    public class Company : Entity, ICompany
    {
        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the trade name of the company.
        /// </summary>
        public string? TradeName { get; set; }
        
        /// <summary>
        /// Gets or sets the CNPJ (Brazilian company registration number).
        /// Must be unique across the system.
        /// </summary>
        public string CNPJ { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the company email address.
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Gets or sets the company phone number.
        /// </summary>
        public string? Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the company address.
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city where the company is located.
        /// </summary>
        public string? City { get; set; }
        
        /// <summary>
        /// Gets or sets the state where the company is located.
        /// </summary>
        public string? State { get; set; }
        
        /// <summary>
        /// Gets or sets the ZIP code of the company.
        /// </summary>
        public string? ZipCode { get; set; }
    }
}
