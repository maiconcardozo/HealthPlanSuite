using Foundation.Base.Domain.Implementation;
using HealthPlan.Domain.Interfaces;

namespace HealthPlan.Domain.Entities
{
    /// <summary>
    /// Represents an age range implementation used for health plan pricing.
    /// Age ranges define different pricing tiers based on beneficiary age.
    /// Inherits from Entity base class providing audit fields and implements IAgeRange interface.
    /// </summary>
    public class AgeRange : Entity, IAgeRange
    {
        /// <summary>
        /// Gets or sets the age range ID.
        /// Maps to SQL column: IdAgeRange
        /// </summary>
        public int IdAgeRange 
        { 
            get => Id; 
            set => Id = value; 
        }
        /// <summary>
        /// Gets or sets the description of the age range.
        /// For example: "0-18 anos", "19-23 anos", "24-28 anos", etc.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the minimum age for this range (inclusive).
        /// Maps to SQL column: IdadeMinima
        /// </summary>
        public int MinAge { get; set; }

        /// <summary>
        /// Gets or sets the maximum age for this range (inclusive).
        /// Maps to SQL column: IdadeMaxima
        /// </summary>
        public int MaxAge { get; set; }

        /// <summary>
        /// Gets or sets the multiplier factor for premium calculation.
        /// Maps to SQL column: Multiplicador
        /// Used to calculate age-adjusted premiums.
        /// </summary>
        public decimal Multiplier { get; set; } = 1.0000m;
    }
}
