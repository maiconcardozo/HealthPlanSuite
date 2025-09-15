using Foundation.Base.Domain.Implementation;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a promotional discount implementation for a health plan.
    /// Promotional discounts are temporary price reductions offered on health plans.
    /// Inherits from Entity base class providing audit fields.
    /// </summary>
    public class DescontoPromocional : Entity
    {
        /// <summary>
        /// Gets or sets the health plan ID that this promotional discount applies to.
        /// References the HealthPlan entity.
        /// Maps to SQL column: HealthPlanId
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the health plan that this promotional discount applies to.
        /// Navigation property for HealthPlanId foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage.
        /// The percentage value of the promotional discount (e.g., 10 for 10%).
        /// Maps to SQL column: PercentualDesconto
        /// </summary>
        public decimal PercentualDesconto { get; set; }

        /// <summary>
        /// Gets or sets the start date of validity for this promotional discount.
        /// The date from which this discount becomes effective.
        /// Maps to SQL column: ValidadeInicio
        /// </summary>
        public DateTime ValidadeInicio { get; set; }

        /// <summary>
        /// Gets or sets the end date of validity for this promotional discount.
        /// The date until which this discount remains effective.
        /// Maps to SQL column: ValidadeFim
        /// </summary>
        public DateTime ValidadeFim { get; set; }

        /// <summary>
        /// Gets or sets the observation or description for this promotional discount.
        /// Additional details about the discount terms and conditions.
        /// Maps to SQL column: Observacao
        /// </summary>
        public string? Observacao { get; set; }
    }
}