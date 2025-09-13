using HealthPlan.Quote.Domain.Base;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents an age range used for health plan pricing.
    /// Age ranges define different pricing tiers based on beneficiary age.
    /// </summary>
    public interface IAgeRange : IEntity
    {
        /// <summary>
        /// Gets or sets the minimum age for this range (inclusive).
        /// </summary>
        int MinAge { get; set; }

        /// <summary>
        /// Gets or sets the maximum age for this range (inclusive).
        /// </summary>
        int MaxAge { get; set; }

        /// <summary>
        /// Gets or sets the description of the age range.
        /// </summary>
        string Description { get; set; }
    }
}
