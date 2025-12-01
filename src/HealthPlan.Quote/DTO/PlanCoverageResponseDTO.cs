namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for PlanCoverage response operations.
    /// Used for returning PlanCoverage data to API consumers.
    /// </summary>
    public class PlanCoverageResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the plan coverage.
        /// </summary>
        public int Id { get; set; }

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
        /// Gets or sets the date and time when this entity was created.
        /// </summary>
        public DateTime DtCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this entity was deleted (soft delete).
        /// Null if the entity is still active.
        /// </summary>
        public DateTime? DtDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this entity was last updated.
        /// Null if the entity has never been updated.
        /// </summary>
        public DateTime? DtUpdated { get; set; }

        /// <summary>
        /// Gets or sets the user who created this entity.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user who last updated this entity.
        /// Null if the entity has never been updated.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the user who deleted this entity.
        /// Null if the entity is still active.
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}