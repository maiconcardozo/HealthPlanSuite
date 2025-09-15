using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for TaxaAdesao data access operations.
    /// Extends base repository functionality with TaxaAdesao-specific methods.
    /// </summary>
    public interface ITaxaAdesaoRepository : IEntityRepository<TaxaAdesao>
    {
        /// <summary>
        /// Retrieves adhesion fees for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of adhesion fees for the health plan</returns>
        IEnumerable<TaxaAdesao> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets the current valid adhesion fee for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current adhesion fee if found, null otherwise</returns>
        TaxaAdesao? GetCurrentValidFee(int healthPlanId, DateTime date);
    }
}