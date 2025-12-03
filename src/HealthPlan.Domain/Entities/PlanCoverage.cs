using Foundation.Base.Domain.Implementation;
using HealthPlan.Domain.Interfaces;

namespace HealthPlan.Domain.Entities
{
    /// <summary>
    /// Represents the relationship between a health plan and its coverages.
    /// This entity manages which coverages are included in each health plan and their specific values.
    /// Inherits from Entity base class providing audit fields and implements IPlanCoverage interface.
    /// </summary>
    public class PlanCoverage : Entity, IPlanCoverage
    {
        /// <summary>
        /// Gets or sets the plan coverage ID.
        /// Maps to SQL column: IdPlanCoverage
        /// </summary>
        public int IdPlanCoverage 
        { 
            get => Id; 
            set => Id = value; 
        }

        /// <summary>
        /// Gets or sets the health plan ID.
        /// References the HealthPlan entity.
        /// Maps to SQL column: IdHealthPlan
        /// </summary>
        public int IdHealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the health plan.
        /// Navigation property for IdHealthPlan foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the coverage ID.
        /// References the Coverage entity.
        /// Maps to SQL column: IdCoverage
        /// </summary>
        public int IdCoverage { get; set; }

        /// <summary>
        /// Gets or sets the coverage.
        /// Navigation property for IdCoverage foreign key.
        /// </summary>
        public Coverage? Coverage { get; set; }
        
        // DEPRECATED properties for backward compatibility
        /// <summary>
        /// Gets or sets the health plan ID.
        /// DEPRECATED: Use IdHealthPlan instead.
        /// </summary>
        [Obsolete("Use IdHealthPlan instead")]
        public int HealthPlanId 
        { 
            get => IdHealthPlan; 
            set => IdHealthPlan = value; 
        }

        /// <summary>
        /// Gets or sets the coverage ID.
        /// DEPRECATED: Use IdCoverage instead.
        /// </summary>
        [Obsolete("Use IdCoverage instead")]
        public int CoverageId 
        { 
            get => IdCoverage; 
            set => IdCoverage = value; 
        }

        /// <summary>
        /// Gets or sets the premium value for this coverage in this plan.
        /// Maps to SQL column: ValorPremio
        /// </summary>
        public decimal PremiumValue { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets whether the coverage is included in the plan.
        /// Maps to SQL column: IsIncluida
        /// </summary>
        public bool IsIncluded { get; set; } = true;
    }
}