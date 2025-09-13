using HealthPlan.Quote.Domain.Interface;
using HealthPlan.Quote.Domain.Base;

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
        /// Gets or sets the name of the coverage.
        /// For example: "Consultas Médicas", "Exames Laboratoriais", "Cirurgias".
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the description of the coverage.
        /// Detailed explanation of what is covered.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of coverage.
        /// Possible values: Ambulatorial, Hospitalar, Obstétrico, Odontológico.
        /// </summary>
        public string CoverageType { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a list of IDs for bulk operations.
        /// Used for operations that require multiple coverage IDs.
        /// </summary>
        public List<int>? LstId { get; set; }
    }
}