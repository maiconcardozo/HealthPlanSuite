using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents an accommodation implementation for health plan hospitalizations.
    /// Accommodations define the type of hospital room/service level available.
    /// Inherits from Entity base class providing audit fields and implements IAccommodation interface.
    /// </summary>
    public class Accommodation : Entity, IAccommodation
    {
        /// <summary>
        /// Gets or sets the type of accommodation.
        /// Examples: "Enfermaria", "Apartamento", "Apartamento Luxo", "UTI".
        /// Maps to SQL column: Tipo
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the accommodation.
        /// Detailed explanation of the accommodation features.
        /// Maps to SQL column: Descricao
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the additional value for this accommodation type.
        /// Maps to SQL column: ValorAdicional
        /// </summary>
        public decimal AdditionalValue { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets the collection of health plans using this accommodation.
        /// Navigation property for the relationship with HealthPlan.
        /// </summary>
        public ICollection<HealthPlan>? HealthPlans { get; set; }
    }
}