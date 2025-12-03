using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Accommodation data access operations.
    /// Extends base repository functionality with Accommodation-specific methods.
    /// </summary>
    public interface IAccommodationRepository : IEntityRepository<Accommodation>
    {
        /// <summary>
        /// Retrieves accommodations by type.
        /// </summary>
        /// <param name="type">Accommodation type to filter by</param>
        /// <returns>Collection of accommodations of the specified type</returns>
        IEnumerable<Accommodation> GetByType(string type);
        
        /// <summary>
        /// Retrieves accommodations with additional value within a range.
        /// </summary>
        /// <param name="minValue">Minimum additional value</param>
        /// <param name="maxValue">Maximum additional value</param>
        /// <returns>Collection of accommodations within the specified value range</returns>
        IEnumerable<Accommodation> GetByValueRange(decimal minValue, decimal maxValue);
        
        /// <summary>
        /// Checks if an accommodation type already exists.
        /// </summary>
        /// <param name="type">Accommodation type to check</param>
        /// <returns>True if the type exists, false otherwise</returns>
        bool TypeExists(string type);
        
        /// <summary>
        /// Checks if an accommodation type exists for a different accommodation (used during updates).
        /// </summary>
        /// <param name="type">Accommodation type to check</param>
        /// <param name="excludeId">Accommodation ID to exclude from the check</param>
        /// <returns>True if the type exists for another accommodation, false otherwise</returns>
        bool TypeExistsForDifferentAccommodation(string type, int excludeId);
    }
}