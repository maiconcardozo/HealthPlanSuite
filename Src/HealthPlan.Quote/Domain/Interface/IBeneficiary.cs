using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents a beneficiary who can be covered by health plans.
    /// Beneficiaries are individuals who receive health insurance coverage.
    /// </summary>
    public interface IBeneficiary : IEntity
    {
        /// <summary>
        /// Gets or sets the beneficiary's full name.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry).
        /// </summary>
        string CPF { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's email address.
        /// </summary>
        string? Email { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's phone number.
        /// </summary>
        string? Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's date of birth.
        /// </summary>
        DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's gender.
        /// </summary>
        string? Gender { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's address.
        /// </summary>
        string? Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city where the beneficiary lives.
        /// </summary>
        string? City { get; set; }
        
        /// <summary>
        /// Gets or sets the state where the beneficiary lives.
        /// </summary>
        string? State { get; set; }
        
        /// <summary>
        /// Gets or sets the ZIP code of the beneficiary.
        /// </summary>
        string? ZipCode { get; set; }
    }
}