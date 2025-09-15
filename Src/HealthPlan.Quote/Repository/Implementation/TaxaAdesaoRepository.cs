using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for TaxaAdesao management operations.
    /// Provides concrete data access methods for TaxaAdesao following the repository pattern.
    /// </summary>
    public class TaxaAdesaoRepository : EntityRepository<TaxaAdesao>, ITaxaAdesaoRepository
    {
        /// <summary>
        /// Initializes a new instance of the TaxaAdesaoRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public TaxaAdesaoRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves adhesion fees for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of adhesion fees for the health plan</returns>
        public IEnumerable<TaxaAdesao> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<TaxaAdesao>()
                .Where(ta => ta.HealthPlanId == healthPlanId)
                .OrderBy(ta => ta.ValidadeInicio)
                .ToList();
        }

        /// <summary>
        /// Gets the current valid adhesion fee for a health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current adhesion fee if found, null otherwise</returns>
        public TaxaAdesao? GetCurrentValidFee(int healthPlanId, DateTime date)
        {
            return _context.Set<TaxaAdesao>()
                .FirstOrDefault(ta => ta.HealthPlanId == healthPlanId 
                    && ta.ValidadeInicio <= date 
                    && ta.ValidadeFim >= date 
                    && ta.IsActive);
        }
    }
}