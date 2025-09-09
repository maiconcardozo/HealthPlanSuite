using HealthPlan.Quote.Domain.Interface;
using Foundation.Base.Domain.Implementation;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a health insurance plan implementation offered by a company.
    /// Health plans define the coverage and terms for health insurance.
    /// Inherits from Entity base class providing audit fields and implements IHealthPlan interface.
    /// </summary>
    public class HealthPlan : Entity, IHealthPlan
    {
        /// <summary>
        /// Gets or sets the company ID that offers this health plan.
        /// References the Company entity.
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the health plan.
        /// For example: "Plano Básico", "Plano Executivo", "Plano Premium".
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the unique code of the health plan.
        /// Must be unique across the system.
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the description of the health plan.
        /// Detailed explanation of the plan benefits and coverage.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the health plan.
        /// Possible values: Individual, Familiar, Empresarial.
        /// </summary>
        public string PlanType { get; set; } = string.Empty;
    }
}