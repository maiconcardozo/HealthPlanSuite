namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for HealthPlan response operations.
    /// Used for returning HealthPlan data to API consumers.
    /// </summary>
    public class HealthPlanResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the health plan.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the company ID that offers this health plan.
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the health plan.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the unique code of the health plan.
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the description of the health plan.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the health plan.
        /// </summary>
        public string PlanType { get; set; } = string.Empty;
        
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