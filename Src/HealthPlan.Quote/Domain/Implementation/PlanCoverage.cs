using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
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
        /// Maps to SQL column: IdPlanoCobertura
        /// </summary>
        public int IdPlanoCobertura 
        { 
            get => Id; 
            set => Id = value; 
        }

        /// <summary>
        /// Gets or sets the health plan ID.
        /// References the HealthPlan entity.
        /// Maps to SQL column: IdPlanoSaude
        /// </summary>
        public int IdPlanoSaude { get; set; }

        /// <summary>
        /// Gets or sets the health plan.
        /// Navigation property for IdPlanoSaude foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the coverage ID.
        /// References the Coverage entity.
        /// Maps to SQL column: IdCobertura
        /// </summary>
        public int IdCobertura { get; set; }

        /// <summary>
        /// Gets or sets the coverage.
        /// Navigation property for IdCobertura foreign key.
        /// </summary>
        public Coverage? Coverage { get; set; }
        
        // DEPRECATED properties for backward compatibility
        /// <summary>
        /// Gets or sets the health plan ID.
        /// DEPRECATED: Use IdPlanoSaude instead.
        /// </summary>
        [Obsolete("Use IdPlanoSaude instead")]
        public int HealthPlanId 
        { 
            get => IdPlanoSaude; 
            set => IdPlanoSaude = value; 
        }

        /// <summary>
        /// Gets or sets the coverage ID.
        /// DEPRECATED: Use IdCobertura instead.
        /// </summary>
        [Obsolete("Use IdCobertura instead")]
        public int CoverageId 
        { 
            get => IdCobertura; 
            set => IdCobertura = value; 
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