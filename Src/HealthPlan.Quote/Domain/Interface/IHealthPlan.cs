using HealthPlan.Quote.Domain.Base;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents a health insurance plan offered by a company.
    /// Health plans define the coverage and terms for health insurance.
    /// </summary>
    public interface IHealthPlan : IEntity
    {
        /// <summary>
        /// Gets or sets the company ID that offers this health plan.
        /// </summary>
        int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the health plan.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the unique code of the health plan.
        /// </summary>
        string Code { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the health plan.
        /// </summary>
        string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the health plan.
        /// </summary>
        string PlanType { get; set; }
    }
}