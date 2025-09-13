using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for AgeRange data access operations.
    /// Extends base repository functionality with AgeRange-specific methods.
    /// </summary>
    public interface IAgeRangeRepository : IEntityRepository<AgeRange>
    {
        /// <summary>
        /// Finds age range that contains the specified age.
        /// </summary>
        /// <param name="age">Age to search for</param>
        /// <returns>AgeRange if found, null otherwise</returns>
        AgeRange? GetByAge(int age);
        
        /// <summary>
        /// Retrieves age ranges that overlap with the specified range.
        /// </summary>
        /// <param name="minAge">Minimum age</param>
        /// <param name="maxAge">Maximum age</param>
        /// <returns>Collection of overlapping age ranges</returns>
        IEnumerable<AgeRange> GetOverlappingRanges(int minAge, int maxAge);
        
        /// <summary>
        /// Checks if an age range overlaps with existing ranges.
        /// </summary>
        /// <param name="minAge">Minimum age</param>
        /// <param name="maxAge">Maximum age</param>
        /// <returns>True if overlaps exist, false otherwise</returns>
        bool HasOverlappingRanges(int minAge, int maxAge);
        
        /// <summary>
        /// Checks if an age range overlaps with existing ranges excluding a specific range.
        /// </summary>
        /// <param name="minAge">Minimum age</param>
        /// <param name="maxAge">Maximum age</param>
        /// <param name="excludeId">AgeRange ID to exclude from check</param>
        /// <returns>True if overlaps exist, false otherwise</returns>
        bool HasOverlappingRangesForUpdate(int minAge, int maxAge, int excludeId);
    }
}