using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for HealthPlan data access operations.
    /// Extends base repository functionality with HealthPlan-specific methods.
    /// </summary>
    public interface IHealthPlanRepository : IEntityRepository<Domain.Implementation.HealthPlan>
    {
    }
}