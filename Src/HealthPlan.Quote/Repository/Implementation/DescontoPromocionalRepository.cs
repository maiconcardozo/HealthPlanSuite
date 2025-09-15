using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for DescontoPromocional management operations.
    /// Provides concrete data access methods for DescontoPromocional following the repository pattern.
    /// </summary>
    public class DescontoPromocionalRepository : EntityRepository<DescontoPromocional>, IDescontoPromocionalRepository
    {
        /// <summary>
        /// Initializes a new instance of the DescontoPromocionalRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public DescontoPromocionalRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves promotional discounts for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of promotional discounts for the health plan</returns>
        public IEnumerable<DescontoPromocional> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<DescontoPromocional>()
                .Where(dp => dp.HealthPlanId == healthPlanId)
                .OrderBy(dp => dp.ValidadeInicio)
                .ToList();
        }

        /// <summary>
        /// Gets the current valid promotional discount for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current promotional discount if found, null otherwise</returns>
        public DescontoPromocional? GetCurrentValidDiscount(int healthPlanId, DateTime date)
        {
            return _context.Set<DescontoPromocional>()
                .FirstOrDefault(dp => dp.HealthPlanId == healthPlanId 
                    && dp.ValidadeInicio <= date 
                    && dp.ValidadeFim >= date 
                    && dp.IsActive);
        }

        /// <summary>
        /// Gets all active promotional discounts within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active promotional discounts</returns>
        public IEnumerable<DescontoPromocional> GetActiveDiscountsInPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Set<DescontoPromocional>()
                .Where(dp => dp.ValidadeInicio <= endDate 
                    && dp.ValidadeFim >= startDate 
                    && dp.IsActive)
                .OrderBy(dp => dp.HealthPlanId)
                .ThenBy(dp => dp.ValidadeInicio)
                .ToList();
        }
    }
}