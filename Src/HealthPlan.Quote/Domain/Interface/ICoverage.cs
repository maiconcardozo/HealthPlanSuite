using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents a type of medical coverage provided by health plans.
    /// Coverage defines what medical services are included in a health plan.
    /// </summary>
    public interface ICoverage : IEntity
    {
        /// <summary>
        /// Gets or sets the name of the coverage.
        /// </summary>
        string Name { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the coverage.
        /// </summary>
        string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of coverage.
        /// </summary>
        string CoverageType { get; set; }
    }
}