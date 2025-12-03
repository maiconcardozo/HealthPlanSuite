using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for PlanPriceRange data access operations.
    /// Extends base repository functionality with PlanPriceRange-specific methods.
    /// </summary>
    public interface IPlanPriceRangeRepository : IEntityRepository<PlanPriceRange>
    {
        /// <summary>
        /// Retrieves plan price ranges for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of plan price ranges for the health plan</returns>
        IEnumerable<PlanPriceRange> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets plan price ranges for a specific age range.
        /// </summary>
        /// <param name="ageRangeId">Age range ID</param>
        /// <returns>Collection of plan price ranges for the age range</returns>
        IEnumerable<PlanPriceRange> GetByAgeRangeId(int ageRangeId);
        
        /// <summary>
        /// Gets the current valid price for a health plan and age range.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="ageRangeId">Age range ID</param>
        /// <param name="tipoContratacao">Contract type</param>
        /// <param name="tipoCoparticipacao">Co-participation type</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current plan price range if found, null otherwise</returns>
        PlanPriceRange? GetCurrentValidPrice(int healthPlanId, int ageRangeId, string tipoContratacao, string tipoCoparticipacao, DateTime date);
        
        /// <summary>
        /// Gets all active plan price ranges within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active plan price ranges</returns>
        IEnumerable<PlanPriceRange> GetActivePricesInPeriod(DateTime startDate, DateTime endDate);
    }
}