using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a type of medical coverage implementation provided by health plans.
    /// Coverage defines what medical services are included in a health plan.
    /// Inherits from Entity base class providing audit fields and implements ICoverage interface.
    /// </summary>
    public class Coverage : Entity, ICoverage
    {
        /// <summary>
        /// Gets or sets the coverage ID.
        /// Maps to SQL column: IdCobertura
        /// </summary>
        public int IdCobertura 
        { 
            get => Id; 
            set => Id = value; 
        }
        /// <summary>
        /// Gets or sets the name of the coverage.
        /// For example: "Consultas Médicas", "Exames Laboratoriais", "Cirurgias".
        /// Maps to SQL column: Nome
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the coverage.
        /// Detailed explanation of what is covered.
        /// Maps to SQL column: Descricao
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the type of coverage.
        /// Possible values: Ambulatorial, Hospitalar, Obstétrico, Odontológico, Emergencial.
        /// Maps to SQL column: Tipo
        /// </summary>
        public string CoverageType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the base value of the coverage.
        /// Maps to SQL column: ValorBase
        /// </summary>
        public decimal BaseValue { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets whether this coverage is mandatory.
        /// Maps to SQL column: IsObrigatoria
        /// </summary>
        public bool IsMandatory { get; set; } = false;

        /// <summary>
        /// Gets or sets a list of IDs for bulk operations.
        /// Used for operations that require multiple coverage IDs.
        /// </summary>
        public new IEnumerable<int>? LstId { get; set; }
    }
}
