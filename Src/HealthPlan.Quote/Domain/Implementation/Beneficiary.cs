using HealthPlan.Quote.Domain.Interface;
using Foundation.Base.Domain.Implemetation;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a beneficiary implementation who can be covered by health plans.
    /// Beneficiaries are individuals who receive health insurance coverage.
    /// Inherits from Entity base class providing audit fields and implements IBeneficiary interface.
    /// </summary>
    public class Beneficiary : Entity, IBeneficiary
    {
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
    }
}