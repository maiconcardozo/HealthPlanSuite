using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Repositories;
using HealthPlan.Infrastructure.Persistence;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for HealthPlan management operations.
    /// Provides concrete data access methods for HealthPlan following the repository pattern.
    /// </summary>
    public class HealthPlanRepository : EntityRepository<Domain.Entities.HealthPlan>, IHealthPlanRepository
    {
        /// <summary>
        /// Initializes a new instance of the HealthPlanRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public HealthPlanRepository(IApplicationContext context) : base(context)
        {
        }
    }
}