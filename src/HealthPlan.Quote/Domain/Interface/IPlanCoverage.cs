using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents the relationship between a health plan and its coverages.
    /// This interface manages which coverages are included in each health plan and their specific values.
    /// </summary>
    public interface IPlanCoverage : IEntity
    {
        /// <summary>
        /// Gets or sets the health plan ID.
        /// </summary>
        int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the coverage ID.
        /// </summary>
        int CoverageId { get; set; }

        /// <summary>
        /// Gets or sets the premium value for this coverage in this plan.
        /// </summary>
        decimal PremiumValue { get; set; }

        /// <summary>
        /// Gets or sets whether the coverage is included in the plan.
        /// </summary>
        bool IsIncluded { get; set; }
    }
}