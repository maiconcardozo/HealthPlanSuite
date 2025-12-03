namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Coverage response operations.
    /// Used for returning Coverage data to API consumers.
    /// </summary>
    public class CoverageResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the coverage.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the coverage.
        /// For example: "Medical Consultations", "Laboratory Tests", "Surgeries".
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the description of the coverage.
        /// Detailed explanation of what is covered.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of coverage.
        /// Possible values: Outpatient, Hospital, Obstetric, Dental.
        /// </summary>
        public string CoverageType { get; set; } = string.Empty;
        
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