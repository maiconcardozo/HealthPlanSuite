using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for Coverage management operations.
    /// Provides concrete data access methods for Coverage following the repository pattern.
    /// </summary>
    public class CoverageRepository : EntityRepository<Coverage>, ICoverageRepository
    {
        private readonly IApplicationContext _context;

        /// <summary>
        /// Initializes a new instance of the CoverageRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public CoverageRepository(IApplicationContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves coverages by name (partial match).
        /// </summary>
        /// <param name="name">Coverage name or part of name</param>
        /// <returns>Collection of coverages matching the name criteria</returns>
        public IEnumerable<Coverage> GetByName(string name)
        {
            return _context.Set<Coverage>()
                .Where(c => c.Name.Contains(name))
                .OrderBy(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Retrieves coverages by type.
        /// </summary>
        /// <param name="coverageType">Coverage type</param>
        /// <returns>Collection of coverages of the specified type</returns>
        public IEnumerable<Coverage> GetByCoverageType(string coverageType)
        {
            return _context.Set<Coverage>()
                .Where(c => c.CoverageType == coverageType)
                .OrderBy(c => c.Name)
                .ToList();
        }

        /// <summary>
        /// Retrieves all available coverage types.
        /// </summary>
        /// <returns>Collection of distinct coverage types</returns>
        public IEnumerable<string> GetAllCoverageTypes()
        {
            return _context.Set<Coverage>()
                .Where(c => c.IsActive)
                .Select(c => c.CoverageType)
                .Distinct()
                .OrderBy(t => t)
                .ToList();
        }

        /// <summary>
        /// Checks if a coverage name already exists.
        /// </summary>
        /// <param name="name">Coverage name to check</param>
        /// <returns>True if the name exists, false otherwise</returns>
        public bool NameExists(string name)
        {
            return _context.Set<Coverage>().Any(c => c.Name == name);
        }

        /// <summary>
        /// Checks if a coverage name exists for a different coverage (used during updates).
        /// </summary>
        /// <param name="name">Coverage name to check</param>
        /// <param name="excludeId">Coverage ID to exclude from the check</param>
        /// <returns>True if the name exists for another coverage, false otherwise</returns>
        public bool NameExistsForDifferentCoverage(string name, int excludeId)
        {
            return _context.Set<Coverage>().Any(c => c.Name == name && c.Id != excludeId);
        }
    }
}