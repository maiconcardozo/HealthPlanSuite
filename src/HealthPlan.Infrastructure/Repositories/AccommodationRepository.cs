using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Accommodation management operations.
    /// </summary>
    public class AccommodationRepository : EntityRepository<Accommodation>, IAccommodationRepository
    {
        public AccommodationRepository(IApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Accommodation> GetByType(string type)
        {
            return _context.Set<Accommodation>()
                .Where(a => a.Type == type && a.IsActive)
                .ToList();
        }

        public IEnumerable<Accommodation> GetByValueRange(decimal minValue, decimal maxValue)
        {
            return _context.Set<Accommodation>()
                .Where(a => a.AdditionalValue >= minValue && a.AdditionalValue <= maxValue && a.IsActive)
                .ToList();
        }

        public bool TypeExists(string type)
        {
            return _context.Set<Accommodation>()
                .Any(a => a.Type == type && a.IsActive);
        }

        public bool TypeExistsForDifferentAccommodation(string type, int excludeId)
        {
            return _context.Set<Accommodation>()
                .Any(a => a.Type == type && a.Id != excludeId && a.IsActive);
        }
    }
}
