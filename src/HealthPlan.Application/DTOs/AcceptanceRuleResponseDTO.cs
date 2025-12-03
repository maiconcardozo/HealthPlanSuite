namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for AcceptanceRule response operations.
    /// Used for returning AcceptanceRule data to API consumers.
    /// </summary>
    public class AcceptanceRuleResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the acceptance rule.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the health plan ID this rule applies to.
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the type of rule.
        /// Examples: "Age", "Income", "Profession", "Marital Status".
        /// </summary>
        public string RuleType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the operator for the rule.
        /// Possible values: "=", ">", "<", ">=", "<=", "BETWEEN", "IN".
        /// </summary>
        public string Operator { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the minimum value for the rule.
        /// </summary>
        public string? MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for the rule.
        /// </summary>
        public string? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the list of accepted values (JSON format).
        /// Used for "IN" operator rules.
        /// </summary>
        public string? ValuesList { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rejection message displayed when rule is not met.
        /// </summary>
        public string? RejectionMessage { get; set; }

        /// <summary>
        /// Gets or sets whether this rule is mandatory.
        /// </summary>
        public bool IsMandatory { get; set; } = true;

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