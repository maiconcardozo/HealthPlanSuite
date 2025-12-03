using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Repositories;
using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for PlanPriceRange management operations.
    /// Provides concrete data access methods for PlanPriceRange following the repository pattern.
    /// </summary>
    public class PlanPriceRangeRepository : EntityRepository<PlanPriceRange>, IPlanPriceRangeRepository
    {
        /// <summary>
        /// Initializes a new instance of the PlanPriceRangeRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public PlanPriceRangeRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves plan price ranges for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of plan price ranges for the health plan</returns>
        public IEnumerable<PlanPriceRange> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<PlanPriceRange>()
                .Where(ppf => ppf.HealthPlanId == healthPlanId)
                .OrderBy(ppf => ppf.AgeRangeId)
                .ThenBy(ppf => ppf.ContractType)
                .ThenBy(ppf => ppf.ValidityStart)
                .ToList();
        }

        /// <summary>
        /// Gets plan price ranges for a specific age range.
        /// </summary>
        /// <param name="ageRangeId">Age range ID</param>
        /// <returns>Collection of plan price ranges for the age range</returns>
        public IEnumerable<PlanPriceRange> GetByAgeRangeId(int ageRangeId)
        {
            return _context.Set<PlanPriceRange>()
                .Where(ppf => ppf.AgeRangeId == ageRangeId)
                .OrderBy(ppf => ppf.HealthPlanId)
                .ThenBy(ppf => ppf.ContractType)
                .ThenBy(ppf => ppf.ValidityStart)
                .ToList();
        }

        /// <summary>
        /// Gets the current valid price for a health plan and age range.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="ageRangeId">Age range ID</param>
        /// <param name="tipoContratacao">Contract type</param>
        /// <param name="tipoCoparticipacao">Co-participation type</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current plan price range if found, null otherwise</returns>
        public PlanPriceRange? GetCurrentValidPrice(int healthPlanId, int ageRangeId, string tipoContratacao, string tipoCoparticipacao, DateTime date)
        {
            return _context.Set<PlanPriceRange>()
                .FirstOrDefault(ppf => ppf.HealthPlanId == healthPlanId 
                    && ppf.AgeRangeId == ageRangeId
                    && ppf.ContractType == tipoContratacao
                    && ppf.CoparticipationType == tipoCoparticipacao
                    && ppf.ValidityStart <= date 
                    && ppf.ValidityEnd >= date 
                    && ppf.IsActive);
        }

        /// <summary>
        /// Gets all active plan price ranges within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active plan price ranges</returns>
        public IEnumerable<PlanPriceRange> GetActivePricesInPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Set<PlanPriceRange>()
                .Where(ppf => ppf.ValidityStart <= endDate 
                    && ppf.ValidityEnd >= startDate 
                    && ppf.IsActive)
                .OrderBy(ppf => ppf.HealthPlanId)
                .ThenBy(ppf => ppf.AgeRangeId)
                .ThenBy(ppf => ppf.ValidityStart)
                .ToList();
        }
    }
}