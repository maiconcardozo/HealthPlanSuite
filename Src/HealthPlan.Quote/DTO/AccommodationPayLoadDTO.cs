namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Accommodation payload operations.
    /// Used for creating and updating Accommodation instances.
    /// </summary>
    public class AccommodationPayLoadDTO
    {
        /// <summary>
        /// Gets or sets the type of accommodation.
        /// Examples: "Enfermaria", "Apartamento", "Apartamento Luxo", "UTI".
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the accommodation.
        /// Detailed explanation of the accommodation features.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the additional value for this accommodation type.
        /// </summary>
        public decimal AdditionalValue { get; set; } = 0.00m;

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