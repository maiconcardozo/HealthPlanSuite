using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for AdhesionFee management operations.
    /// Provides concrete data access methods for AdhesionFee following the repository pattern.
    /// </summary>
    public class AdhesionFeeRepository : EntityRepository<AdhesionFee>, IAdhesionFeeRepository
    {
        /// <summary>
        /// Initializes a new instance of the AdhesionFeeRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public AdhesionFeeRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves adhesion fees for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of adhesion fees for the health plan</returns>
        public IEnumerable<AdhesionFee> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<AdhesionFee>()
                .Where(ta => ta.HealthPlanId == healthPlanId)
                .OrderBy(ta => ta.ValidityStart)
                .ToList();
        }

        /// <summary>
        /// Gets the current valid adhesion fee for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current adhesion fee if found, null otherwise</returns>
        public AdhesionFee? GetCurrentValidFee(int healthPlanId, DateTime date)
        {
            return _context.Set<AdhesionFee>()
                .FirstOrDefault(ta => ta.HealthPlanId == healthPlanId 
                    && ta.ValidityStart <= date 
                    && ta.ValidityEnd >= date 
                    && ta.IsActive);
        }
    }
}