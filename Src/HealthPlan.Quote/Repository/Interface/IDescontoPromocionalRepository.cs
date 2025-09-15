using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for DescontoPromocional data access operations.
    /// Extends base repository functionality with DescontoPromocional-specific methods.
    /// </summary>
    public interface IDescontoPromocionalRepository : IEntityRepository<DescontoPromocional>
    {
        /// <summary>
        /// Retrieves promotional discounts for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of promotional discounts for the health plan</returns>
        IEnumerable<DescontoPromocional> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets the current valid promotional discount for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current promotional discount if found, null otherwise</returns>
        DescontoPromocional? GetCurrentValidDiscount(int healthPlanId, DateTime date);
        
        /// <summary>
        /// Gets all active promotional discounts within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active promotional discounts</returns>
        IEnumerable<DescontoPromocional> GetActiveDiscountsInPeriod(DateTime startDate, DateTime endDate);
    }
}