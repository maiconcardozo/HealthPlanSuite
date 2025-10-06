namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Beneficiary payload operations.
    /// Used for creating and updating Beneficiary instances.
    /// </summary>
    public class BeneficiaryPayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the beneficiary.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the beneficiary's full name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry).
        /// Must be unique across the system.
        /// </summary>
        public string CPF { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the beneficiary's email address.
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's phone number.
        /// </summary>
        public string? Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's date of birth.
        /// Used for age calculation and premium determination.
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's gender.
        /// Possible values: M (Male), F (Female), Other.
        /// </summary>
        public string? Gender { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's address.
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city where the beneficiary lives.
        /// </summary>
        public string? City { get; set; }
        
        /// <summary>
        /// Gets or sets the state where the beneficiary lives.
        /// </summary>
        public string? State { get; set; }
        
        /// <summary>
        /// Gets or sets the ZIP code of the beneficiary.
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