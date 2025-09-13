using HealthPlan.Quote.Domain.Interface;
using Foundation.Base.Domain.Implemetation;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents an age range implementation used for health plan pricing.
    /// Age ranges define different pricing tiers based on beneficiary age.
    /// Inherits from Entity base class providing audit fields and implements IAgeRange interface.
    /// </summary>
    public class AgeRange : Entity, IAgeRange
    {
        /// <summary>
        /// Gets or sets the minimum age for this range (inclusive).
        /// </summary>
        public int MinAge { get; set; }
        
        /// <summary>
        /// Gets or sets the maximum age for this range (inclusive).
        /// </summary>
        public int MaxAge { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the age range.
        /// For example: "0-18 anos", "19-23 anos", "24-28 anos", etc.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }
}