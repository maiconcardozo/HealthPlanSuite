using System.Linq.Expressions;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for Coverage management operations.
    /// Provides comprehensive Coverage CRUD operations following service layer patterns.
    /// </summary>
    public interface ICoverageService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all coverages from the system.
        /// </summary>
        /// <returns>Collection of all coverage entities</returns>
        IEnumerable<Coverage> GetAllCoverages();
        
        /// <summary>
        /// Retrieves a coverage by its unique identifier.
        /// </summary>
        /// <param name="id">Coverage ID</param>
        /// <returns>Coverage if found, null otherwise</returns>
        Coverage? GetById(int id);
        
        /// <summary>
        /// Retrieves multiple coverages by their IDs.
        /// </summary>
        /// <param name="coverageIds">Collection of coverage IDs</param>
        /// <returns>Collection of matching coverage entities</returns>
        IEnumerable<Coverage> GetCoveragesByIds(IEnumerable<int> coverageIds);
        
        /// <summary>
        /// Retrieves coverages by name (partial match).
        /// </summary>
        /// <param name="name">Coverage name or part of name</param>
        /// <returns>Collection of coverages matching the name criteria</returns>
        IEnumerable<Coverage> GetCoveragesByName(string name);
        
        /// <summary>
        /// Retrieves coverages by type.
        /// </summary>
        /// <param name="coverageType">Coverage type</param>
        /// <returns>Collection of coverages of the specified type</returns>
        IEnumerable<Coverage> GetCoveragesByType(string coverageType);
        
        /// <summary>
        /// Retrieves all available coverage types.
        /// </summary>
        /// <returns>Collection of distinct coverage types</returns>
        IEnumerable<string> GetAllCoverageTypes();
        
        /// <summary>
        /// Retrieves coverages that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter coverages</param>
        /// <returns>Collection of matching coverage entities</returns>
        IEnumerable<Coverage> GetCoverages(Expression<Func<Coverage, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single coverage that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter coverages</param>
        /// <returns>Single matching coverage or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple coverages match the predicate</exception>
        Coverage? GetSingleOrDefaultCoverage(Expression<Func<Coverage, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active coverages.
        /// </summary>
        /// <returns>Collection of active coverages</returns>
        IEnumerable<Coverage> GetAllActiveCoverages();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new coverage in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="coverage">Coverage to create</param>
        void AddCoverage(Coverage coverage);
        
        /// <summary>
        /// Creates multiple coverages in a single transaction.
        /// </summary>
        /// <param name="coverages">Collection of coverage entities to create</param>
        void AddCoverages(IEnumerable<Coverage> coverages);
        
        /// <summary>
        /// Updates an existing coverage.
        /// </summary>
        /// <param name="coverage">Coverage with updated information</param>
        void UpdateCoverage(Coverage coverage);
        
        /// <summary>
        /// Deletes a coverage.
        /// </summary>
        /// <param name="coverage">Coverage to delete</param>
        void DeleteCoverage(Coverage coverage);
        
        /// <summary>
        /// Deletes a coverage by its ID.
        /// </summary>
        /// <param name="id">Coverage ID to delete</param>
        void DeleteCoverage(int id);
        
        /// <summary>
        /// Deletes multiple coverage entities.
        /// </summary>
        /// <param name="coverages">Collection of coverage entities to delete</param>
        void DeleteCoverages(IEnumerable<Coverage> coverages);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Validates if a coverage name is unique.
        /// </summary>
        /// <param name="name">Coverage name to validate</param>
        /// <returns>True if name is unique, false otherwise</returns>
        bool IsNameUnique(string name);
        
        /// <summary>
        /// Validates if a coverage name is unique for updates (excludes current entity).
        /// </summary>
        /// <param name="name">Coverage name to validate</param>
        /// <param name="excludeId">Coverage ID to exclude from validation</param>
        /// <returns>True if name is unique, false otherwise</returns>
        bool IsNameUniqueForUpdate(string name, int excludeId);
        
        /// <summary>
        /// Validates coverage type.
        /// </summary>
        /// <param name="coverageType">Coverage type to validate</param>
        /// <returns>True if coverage type is valid, false otherwise</returns>
        bool IsValidCoverageType(string coverageType);
        
        #endregion
    }
}