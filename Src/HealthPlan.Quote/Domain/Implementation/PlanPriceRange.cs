using Foundation.Base.Domain.Implementation;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a plan price range implementation for a health plan.
    /// Plan price ranges define pricing based on age ranges, contract types, and co-participation types.
    /// Inherits from Entity base class providing audit fields.
    /// </summary>
    public class PlanPriceRange : Entity
    {
        /// <summary>
        /// Gets or sets the health plan ID that this price range applies to.
        /// References the HealthPlan entity.
        /// Maps to SQL column: HealthPlanId
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the health plan that this price range applies to.
        /// Navigation property for HealthPlanId foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the age range ID that this price applies to.
        /// References the AgeRange entity.
        /// Maps to SQL column: AgeRangeId
        /// </summary>
        public int AgeRangeId { get; set; }

        /// <summary>
        /// Gets or sets the age range that this price applies to.
        /// Navigation property for AgeRangeId foreign key.
        /// </summary>
        public AgeRange? AgeRange { get; set; }

        /// <summary>
        /// Gets or sets the type of contract.
        /// Possible values: "Individual", "Coletivo por Adesão", "Empresarial".
        /// Maps to SQL column: TipoContratacao
        /// </summary>
        public string ContractType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the type of co-participation.
        /// Possible values: "Parcial", "Total", "Sem Coparticipação".
        /// Maps to SQL column: TipoCoparticipacao
        /// </summary>
        public string CoparticipationType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the original price value.
        /// The base price before any discounts are applied.
        /// Maps to SQL column: ValorOriginal
        /// </summary>
        public decimal OriginalValue { get; set; }

        /// <summary>
        /// Gets or sets the discount value.
        /// The amount of discount applied to the original price.
        /// Maps to SQL column: ValorDesconto
        /// </summary>
        public decimal DiscountValue { get; set; }

        /// <summary>
        /// Gets or sets the start date of validity for this price.
        /// The date from which this price becomes effective.
        /// Maps to SQL column: ValidadeInicio
        /// </summary>
        public DateTime ValidityStart { get; set; }

        /// <summary>
        /// Gets or sets the end date of validity for this price.
        /// The date until which this price remains effective.
        /// Maps to SQL column: ValidadeFim
        /// </summary>
        public DateTime ValidityEnd { get; set; }
    }
}