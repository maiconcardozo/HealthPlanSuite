namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Accommodation response operations.
    /// Used for returning Accommodation data to API consumers.
    /// </summary>
    public class AccommodationResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the accommodation.
        /// </summary>
        public int Id { get; set; }

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
        /// Gets or sets the date and time when this entity was created.
        /// </summary>
        public DateTime DtCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this entity was deleted (soft delete).
        /// Null if the entity is still active.
        /// </summary>
        public DateTime? DtDeleted { get; set; }

        /// <summary>
        /// Gets or sets the date and time when this entity was last updated.
        /// Null if the entity has never been updated.
        /// </summary>
        public DateTime? DtUpdated { get; set; }

        /// <summary>
        /// Gets or sets the user who created this entity.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user who last updated this entity.
        /// Null if the entity has never been updated.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the user who deleted this entity.
        /// Null if the entity is still active.
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}