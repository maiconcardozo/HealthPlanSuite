namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for PlanCoverage payload operations.
    /// Used for creating and updating PlanCoverage instances.
    /// </summary>
    public class PlanCoveragePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the health plan ID.
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the coverage ID.
        /// </summary>
        public int CoverageId { get; set; }

        /// <summary>
        /// Gets or sets the premium value for this coverage in this plan.
        /// </summary>
        public decimal PremiumValue { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets whether the coverage is included in the plan.
        /// </summary>
        public bool IsIncluded { get; set; } = true;

        /// <summary>
        /// Gets or sets the user who created this entity.
        /// Used for audit trail purposes.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user who last updated this entity.
        /// Used for audit trail purposes during updates.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}