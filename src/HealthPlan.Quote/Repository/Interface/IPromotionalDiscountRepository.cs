using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for PromotionalDiscount data access operations.
    /// Extends base repository functionality with PromotionalDiscount-specific methods.
    /// </summary>
    public interface IPromotionalDiscountRepository : IEntityRepository<PromotionalDiscount>
    {
        /// <summary>
        /// Retrieves promotional discounts for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of promotional discounts for the health plan</returns>
        IEnumerable<PromotionalDiscount> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets the current valid promotional discount for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current promotional discount if found, null otherwise</returns>
        PromotionalDiscount? GetCurrentValidDiscount(int healthPlanId, DateTime date);
        
        /// <summary>
        /// Gets all active promotional discounts within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active promotional discounts</returns>
        IEnumerable<PromotionalDiscount> GetActiveDiscountsInPeriod(DateTime startDate, DateTime endDate);
    }
}