namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for AgeRange response operations.
    /// Used for returning AgeRange data to API consumers.
    /// </summary>
    public class AgeRangeResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the age range.
        /// </summary>
        public int Id { get; set; }
        
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