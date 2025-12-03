using Foundation.Base.Domain.Implementation;

namespace HealthPlan.Domain.Entities
{
    /// <summary>
    /// Represents an adhesion fee implementation for a health plan.
    /// Adhesion fees are one-time charges when joining a health plan.
    /// Inherits from Entity base class providing audit fields.
    /// </summary>
    public class AdhesionFee : Entity
    {
        /// <summary>
        /// Gets or sets the health plan ID that this adhesion fee belongs to.
        /// References the HealthPlan entity.
        /// Maps to SQL column: HealthPlanId
        /// </summary>
        public int HealthPlanId { get; set; }

        /// <summary>
        /// Gets or sets the health plan that this adhesion fee belongs to.
        /// Navigation property for HealthPlanId foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the adhesion fee value.
        /// The monetary amount charged as an adhesion fee for the health plan.
        /// Maps to SQL column: Valor
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// Gets or sets the start date of validity for this adhesion fee.
        /// The date from which this fee becomes effective.
        /// Maps to SQL column: ValidadeInicio
        /// </summary>
        public DateTime ValidityStart { get; set; }

        /// <summary>
        /// Gets or sets the end date of validity for this adhesion fee.
        /// The date until which this fee remains effective.
        /// Maps to SQL column: ValidadeFim
        /// </summary>
        public DateTime ValidityEnd { get; set; }
    }
}