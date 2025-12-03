using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Coverage data access operations.
    /// Extends base repository functionality with Coverage-specific methods.
    /// </summary>
    public interface ICoverageRepository : IEntityRepository<Coverage>
    {
        /// <summary>
        /// Retrieves coverages by name (partial match).
        /// </summary>
        /// <param name="name">Coverage name or part of name</param>
        /// <returns>Collection of coverages matching the name criteria</returns>
        IEnumerable<Coverage> GetByName(string name);
        
        /// <summary>
        /// Retrieves coverages by type.
        /// </summary>
        /// <param name="coverageType">Coverage type</param>
        /// <returns>Collection of coverages of the specified type</returns>
        IEnumerable<Coverage> GetByCoverageType(string coverageType);
        
        /// <summary>
        /// Retrieves all available coverage types.
        /// </summary>
        /// <returns>Collection of distinct coverage types</returns>
        IEnumerable<string> GetAllCoverageTypes();
        
        /// <summary>
        /// Checks if a coverage name already exists.
        /// </summary>
        /// <param name="name">Coverage name to check</param>
        /// <returns>True if the name exists, false otherwise</returns>
        bool NameExists(string name);
        
        /// <summary>
        /// Checks if a coverage name exists for a different coverage (used during updates).
        /// </summary>
        /// <param name="name">Coverage name to check</param>
        /// <param name="excludeId">Coverage ID to exclude from the check</param>
        /// <returns>True if the name exists for another coverage, false otherwise</returns>
        bool NameExistsForDifferentCoverage(string name, int excludeId);

        /// <summary>
        /// Gets coverages by a list of IDs.
        /// </summary>
        /// <param name="coverage">Coverage entity containing list of IDs</param>
        /// <returns>Collection of coverages matching the provided IDs</returns>
        IEnumerable<Coverage> GetByLstId(Coverage coverage);
    }
}