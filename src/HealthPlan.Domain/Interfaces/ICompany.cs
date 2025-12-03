using Foundation.Base.Domain.Interface;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Represents a company that offers health plans.
    /// Companies are the entities that provide health insurance services to beneficiaries.
    /// </summary>
    public interface ICompany : IEntity
    {
        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the trade name of the company.
        /// </summary>
        string? TradeName { get; set; }
        
        /// <summary>
        /// Gets or sets the CNPJ (Brazilian company registration number).
        /// </summary>
        string CNPJ { get; set; }
        
        /// <summary>
        /// Gets or sets the company email address.
        /// </summary>
        string? Email { get; set; }
        
        /// <summary>
        /// Gets or sets the company phone number.
        /// </summary>
        string? Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the company address.
        /// </summary>
        string? Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city where the company is located.
        /// </summary>
        string? City { get; set; }
        
        /// <summary>
        /// Gets or sets the state where the company is located.
        /// </summary>
        string? State { get; set; }
        
        /// <summary>
        /// Gets or sets the ZIP code of the company.
        /// </summary>
        string? ZipCode { get; set; }
    }
}
