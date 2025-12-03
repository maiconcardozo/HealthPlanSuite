using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for PromotionalDiscount management operations.
    /// Provides concrete data access methods for PromotionalDiscount following the repository pattern.
    /// </summary>
    public class PromotionalDiscountRepository : EntityRepository<PromotionalDiscount>, IPromotionalDiscountRepository
    {
        /// <summary>
        /// Initializes a new instance of the PromotionalDiscountRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public PromotionalDiscountRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves promotional discounts for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of promotional discounts for the health plan</returns>
        public IEnumerable<PromotionalDiscount> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<PromotionalDiscount>()
                .Where(dp => dp.HealthPlanId == healthPlanId)
                .OrderBy(dp => dp.ValidityStart)
                .ToList();
        }

        /// <summary>
        /// Gets the current valid promotional discount for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current promotional discount if found, null otherwise</returns>
        public PromotionalDiscount? GetCurrentValidDiscount(int healthPlanId, DateTime date)
        {
            return _context.Set<PromotionalDiscount>()
                .FirstOrDefault(dp => dp.HealthPlanId == healthPlanId 
                    && dp.ValidityStart <= date 
                    && dp.ValidityEnd >= date 
                    && dp.IsActive);
        }

        /// <summary>
        /// Gets all active promotional discounts within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active promotional discounts</returns>
        public IEnumerable<PromotionalDiscount> GetActiveDiscountsInPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Set<PromotionalDiscount>()
                .Where(dp => dp.ValidityStart <= endDate 
                    && dp.ValidityEnd >= startDate 
                    && dp.IsActive)
                .OrderBy(dp => dp.HealthPlanId)
                .ThenBy(dp => dp.ValidityStart)
                .ToList();
        }
    }
}