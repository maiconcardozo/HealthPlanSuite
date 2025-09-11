namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Company response operations.
    /// Used for returning Company data to API consumers.
    /// </summary>
    public class CompanyResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the company.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the trade name of the company.
        /// </summary>
        public string? TradeName { get; set; }
        
        /// <summary>
        /// Gets or sets the CNPJ (Brazilian company registration number).
        /// </summary>
        public string CNPJ { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the company email address.
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Gets or sets the company phone number.
        /// </summary>
        public string? Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the company address.
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city where the company is located.
        /// </summary>
        public string? City { get; set; }
        
        /// <summary>
        /// Gets or sets the state where the company is located.
        /// </summary>
        public string? State { get; set; }
        
        /// <summary>
        /// Gets or sets the ZIP code of the company.
        /// </summary>
        public string? ZipCode { get; set; }
        
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