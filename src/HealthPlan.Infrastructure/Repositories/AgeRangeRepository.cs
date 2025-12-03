using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Repositories;
using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for AgeRange management operations.
    /// Provides concrete data access methods for AgeRange following the repository pattern.
    /// </summary>
    public class AgeRangeRepository : EntityRepository<AgeRange>, IAgeRangeRepository
    {
        /// <summary>
        /// Initializes a new instance of the AgeRangeRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public AgeRangeRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Finds age range that contains the specified age.
        /// </summary>
        /// <param name="age">Age to search for</param>
        /// <returns>AgeRange if found, null otherwise</returns>
        public AgeRange? GetByAge(int age)
        {
            return _context.Set<AgeRange>().FirstOrDefault(ar => ar.MinAge <= age && ar.MaxAge >= age);
        }

        /// <summary>
        /// Retrieves age ranges that overlap with the specified range.
        /// </summary>
        /// <param name="minAge">Minimum age</param>
        /// <param name="maxAge">Maximum age</param>
        /// <returns>Collection of overlapping age ranges</returns>
        public IEnumerable<AgeRange> GetOverlappingRanges(int minAge, int maxAge)
        {
            return _context.Set<AgeRange>()
                .Where(ar => ar.MinAge <= maxAge && ar.MaxAge >= minAge)
                .OrderBy(ar => ar.MinAge)
                .ToList();
        }

        /// <summary>
        /// Checks if an age range overlaps with existing ranges.
        /// </summary>
        /// <param name="minAge">Minimum age</param>
        /// <param name="maxAge">Maximum age</param>
        /// <returns>True if overlaps exist, false otherwise</returns>
        public bool HasOverlappingRanges(int minAge, int maxAge)
        {
            return _context.Set<AgeRange>().Any(ar => ar.MinAge <= maxAge && ar.MaxAge >= minAge);
        }

        /// <summary>
        /// Checks if an age range overlaps with existing ranges excluding a specific range.
        /// </summary>
        /// <param name="minAge">Minimum age</param>
        /// <param name="maxAge">Maximum age</param>
        /// <param name="excludeId">AgeRange ID to exclude from check</param>
        /// <returns>True if overlaps exist, false otherwise</returns>
        public bool HasOverlappingRangesForUpdate(int minAge, int maxAge, int excludeId)
        {
            return _context.Set<AgeRange>().Any(ar => ar.Id != excludeId && ar.MinAge <= maxAge && ar.MaxAge >= minAge);
        }
    }
}