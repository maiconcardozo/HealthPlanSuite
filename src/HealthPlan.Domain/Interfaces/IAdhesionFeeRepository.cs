using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for AdhesionFee data access operations.
    /// Extends base repository functionality with AdhesionFee-specific methods.
    /// </summary>
    public interface IAdhesionFeeRepository : IEntityRepository<AdhesionFee>
    {
        /// <summary>
        /// Retrieves adhesion fees for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of adhesion fees for the health plan</returns>
        IEnumerable<AdhesionFee> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets the current valid adhesion fee for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current adhesion fee if found, null otherwise</returns>
        AdhesionFee? GetCurrentValidFee(int healthPlanId, DateTime date);
    }
}