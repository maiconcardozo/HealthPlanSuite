using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for HealthPlan management operations.
    /// Provides concrete data access methods for HealthPlan following the repository pattern.
    /// </summary>
    public class HealthPlanRepository : EntityRepository<Domain.Implementation.HealthPlan>, IHealthPlanRepository
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