using Foundation.Base.Domain.Interface;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Represents acceptance rules for health plans.
    /// These rules define criteria that beneficiaries must meet to be eligible for a health plan.
    /// </summary>
    public interface IAcceptanceRule : IEntity
    {
        /// <summary>
        /// Gets or sets the health plan ID this rule applies to.
        /// </summary>
        int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the type of rule.
        /// </summary>
        string RuleType { get; set; }

        /// <summary>
        /// Gets or sets the operator for the rule.
        /// </summary>
        string Operator { get; set; }

        /// <summary>
        /// Gets or sets the minimum value for the rule.
        /// </summary>
        string? MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum value for the rule.
        /// </summary>
        string? MaxValue { get; set; }

        /// <summary>
        /// Gets or sets the list of accepted values (JSON format).
        /// </summary>
        string? ValuesList { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the rejection message displayed when rule is not met.
        /// </summary>
        string? RejectionMessage { get; set; }

        /// <summary>
        /// Gets or sets whether this rule is mandatory.
        /// </summary>
        bool IsMandatory { get; set; }
    }
}