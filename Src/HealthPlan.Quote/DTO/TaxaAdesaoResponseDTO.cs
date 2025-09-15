namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for TaxaAdesao response operations.
    /// Used for returning TaxaAdesao data to API consumers.
    /// </summary>
    public class TaxaAdesaoResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the adhesion fee.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID that this adhesion fee belongs to.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the adhesion fee value.
        /// </summary>
        public decimal Valor { get; set; }
        
        /// <summary>
        /// Gets or sets the start date of validity for this adhesion fee.
        /// </summary>
        public DateTime ValidadeInicio { get; set; }
        
        /// <summary>
        /// Gets or sets the end date of validity for this adhesion fee.
        /// </summary>
        public DateTime ValidadeFim { get; set; }
        
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