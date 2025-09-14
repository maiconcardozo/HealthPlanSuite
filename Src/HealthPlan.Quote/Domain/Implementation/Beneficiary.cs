using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

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
        /// Gets or sets the beneficiary ID.
        /// Maps to SQL column: IdBeneficiario
        /// </summary>
        public int IdBeneficiario 
        { 
            get => Id; 
            set => Id = value; 
        }
        /// <summary>
        /// Gets or sets the beneficiary's full name.
        /// Maps to SQL column: Nome
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the CPF (Brazilian individual taxpayer registry).
        /// Must be unique across the system.
        /// Maps to SQL column: CPF
        /// </summary>
        public string CPF { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the beneficiary's email address.
        /// Maps to SQL column: Email
        /// </summary>
        public string? Email { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's phone number.
        /// Maps to SQL column: Telefone
        /// </summary>
        public string? Phone { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's date of birth.
        /// Used for age calculation and premium determination.
        /// Maps to SQL column: DataNascimento
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's gender.
        /// Possible values: M (Male), F (Female).
        /// Maps to SQL column: Sexo
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// Gets or sets the beneficiary's marital status.
        /// Maps to SQL column: EstadoCivil
        /// </summary>
        public string? MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the beneficiary's profession.
        /// Maps to SQL column: Profissao
        /// </summary>
        public string? Profession { get; set; }

        /// <summary>
        /// Gets or sets the family income.
        /// Maps to SQL column: RendaFamiliar
        /// </summary>
        public decimal? FamilyIncome { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary's address.
        /// Maps to SQL column: Endereco
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// Gets or sets the city where the beneficiary lives.
        /// Maps to SQL column: Cidade
        /// </summary>
        public string? City { get; set; }
        
        /// <summary>
        /// Gets or sets the state where the beneficiary lives.
        /// Maps to SQL column: Estado
        /// </summary>
        public string? State { get; set; }
        
        /// <summary>
        /// Gets or sets the ZIP code of the beneficiary.
        /// Maps to SQL column: CEP
        /// </summary>
        public string? ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the mother's name.
        /// Maps to SQL column: NomeMae
        /// </summary>
        public string? MotherName { get; set; }

        /// <summary>
        /// Gets or sets the SUS card number (Brazilian public health system).
        /// Maps to SQL column: CartaoSUS
        /// </summary>
        public string? SUSCardNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of quotes for this beneficiary.
        /// Navigation property for the relationship with Quote.
        /// </summary>
        public ICollection<Quote>? Quotes { get; set; }
    }
}
