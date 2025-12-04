using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for PlanCoverage management operations.
    /// </summary>
    public class PlanCoverageRepository : EntityRepository<PlanCoverage>, IPlanCoverageRepository
    {
        public PlanCoverageRepository(IApplicationContext context) : base(context)
        {
        }

        public IEnumerable<PlanCoverage> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<PlanCoverage>()
                .Where(pc => pc.HealthPlanId == healthPlanId && pc.IsActive)
                .ToList();
        }

        public IEnumerable<PlanCoverage> GetByCoverageId(int coverageId)
        {
            return _context.Set<PlanCoverage>()
                .Where(pc => pc.CoverageId == coverageId && pc.IsActive)
                .ToList();
        }

        public IEnumerable<PlanCoverage> GetIncludedCoverages()
        {
            return _context.Set<PlanCoverage>()
                .Where(pc => pc.IsIncluded && pc.IsActive)
                .ToList();
        }

        public IEnumerable<PlanCoverage> GetByPremiumRange(decimal minValue, decimal maxValue)
        {
            return _context.Set<PlanCoverage>()
                .Where(pc => pc.PremiumValue >= minValue && pc.PremiumValue <= maxValue && pc.IsActive)
                .ToList();
        }

        public bool HealthPlanCoverageCombinationExists(int healthPlanId, int coverageId)
        {
            return _context.Set<PlanCoverage>()
                .Any(pc => pc.HealthPlanId == healthPlanId && pc.CoverageId == coverageId && pc.IsActive);
        }

        public bool HealthPlanCoverageCombinationExistsForDifferentPlanCoverage(int healthPlanId, int coverageId, int excludeId)
        {
            return _context.Set<PlanCoverage>()
                .Any(pc => pc.HealthPlanId == healthPlanId && pc.CoverageId == coverageId && pc.Id != excludeId && pc.IsActive);
        }
    }
}
