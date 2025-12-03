using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for HealthPlan data access operations.
    /// Extends base repository functionality with HealthPlan-specific methods.
    /// </summary>
    public interface IHealthPlanRepository : IEntityRepository<Domain.Entities.HealthPlan>
    {
    }
}