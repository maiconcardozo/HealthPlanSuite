using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a company implementation that offers health plans.
    /// Companies are the entities that provide health insurance services to beneficiaries.
    /// Inherits from Entity base class providing audit fields and implements ICompany interface.
    /// </summary>
    public class Company : Entity, ICompany
    {
        /// <summary>
        /// Gets or sets the company ID.
        /// Maps to SQL column: IdEmpresa
        /// </summary>
        public int IdEmpresa 
        { 
            get => Id; 
            set => Id = value; 
        }
        /// <summary>
        /// Gets or sets the company name.
        /// Maps to SQL column: Nome
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the trade name of the company.
        /// Maps to SQL column: NomeFantasia
        /// </summary>
        public string? TradeName { get; set; }

        /// <summary>
        /// Gets or sets the CNPJ (Brazilian company registration number).
        /// Must be unique across the system.
        /// Maps to SQL column: CNPJ
        /// </summary>
        public string CNPJ { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company email address.
        /// Maps to SQL column: Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the company phone number.
        /// Maps to SQL column: Telefone
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Gets or sets the company address.
        /// Maps to SQL column: Endereco
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Gets or sets the city where the company is located.
        /// Maps to SQL column: Cidade
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Gets or sets the state where the company is located.
        /// Maps to SQL column: Estado
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Gets or sets the ZIP code of the company.
        /// Maps to SQL column: CEP
        /// </summary>
        public string? ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the collection of health plans offered by this company.
        /// Navigation property for the relationship with HealthPlan.
        /// </summary>
        public ICollection<HealthPlan>? HealthPlans { get; set; }

        /// <summary>
        /// Gets or sets the collection of quotes from this company.
        /// Navigation property for the relationship with Quote.
        /// </summary>
        public ICollection<Quote>? Quotes { get; set; }

        /// <summary>
        /// Gets or sets a list of IDs for bulk operations.
        /// Used for operations that require multiple company IDs.
        /// </summary>
        public new IEnumerable<int>? LstId { get; set; }
    }
}
