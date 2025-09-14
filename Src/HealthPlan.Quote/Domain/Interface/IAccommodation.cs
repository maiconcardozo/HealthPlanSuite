using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents an accommodation for health plan hospitalizations.
    /// Accommodations define the type of hospital room/service level available.
    /// </summary>
    public interface IAccommodation : IEntity
    {
        /// <summary>
        /// Gets or sets the type of accommodation.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the accommodation.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the additional value for this accommodation type.
        /// </summary>
        decimal AdditionalValue { get; set; }
    }
}