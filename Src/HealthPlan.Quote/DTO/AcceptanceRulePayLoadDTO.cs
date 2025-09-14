namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for AcceptanceRule payload operations.
    /// Used for creating and updating AcceptanceRule instances.
    /// </summary>
    public class AcceptanceRulePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the health plan ID this rule applies to.
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the type of rule.
        /// Examples: "Idade", "Renda", "Profissão", "Estado Civil".
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