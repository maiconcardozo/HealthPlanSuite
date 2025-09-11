namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Company payload operations.
    /// Used for creating and updating Company instances.
    /// </summary>
    public class CompanyPayLoadDTO
    {
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
        /// Must be unique across the system.
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