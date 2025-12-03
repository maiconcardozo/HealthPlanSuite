namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Beneficiary response operations.
    /// Used for returning Beneficiary data to API consumers.
    /// </summary>
    public class BeneficiaryResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the beneficiary.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's full name.
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry).
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
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's gender.
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