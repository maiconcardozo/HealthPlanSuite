namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for DescontoPromocional response operations.
    /// Used for returning DescontoPromocional data to API consumers.
    /// </summary>
    public class DescontoPromocionalResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the promotional discount.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID that this promotional discount applies to.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the discount percentage.
        /// </summary>
        public decimal PercentualDesconto { get; set; }
        
        /// <summary>
        /// Gets or sets the start date of validity for this promotional discount.
        /// </summary>
        public DateTime ValidadeInicio { get; set; }
        
        /// <summary>
        /// Gets or sets the end date of validity for this promotional discount.
        /// </summary>
        public DateTime ValidadeFim { get; set; }
        
        /// <summary>
        /// Gets or sets the observation or description for this promotional discount.
        /// </summary>
        public string? Observacao { get; set; }
        
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