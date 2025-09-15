using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for PrecoPlanoFaixa data access operations.
    /// Extends base repository functionality with PrecoPlanoFaixa-specific methods.
    /// </summary>
    public interface IPrecoPlanoFaixaRepository : IEntityRepository<PrecoPlanoFaixa>
    {
        /// <summary>
        /// Retrieves plan price ranges for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of plan price ranges for the health plan</returns>
        IEnumerable<PrecoPlanoFaixa> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets plan price ranges for a specific age range.
        /// </summary>
        /// <param name="ageRangeId">Age range ID</param>
        /// <returns>Collection of plan price ranges for the age range</returns>
        IEnumerable<PrecoPlanoFaixa> GetByAgeRangeId(int ageRangeId);
        
        /// <summary>
        /// Gets the current valid price for a health plan and age range.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="ageRangeId">Age range ID</param>
        /// <param name="tipoContratacao">Contract type</param>
        /// <param name="tipoCoparticipacao">Co-participation type</param>
        /// <param name="date">Date to check validity for</param>
        /// <returns>Current plan price range if found, null otherwise</returns>
        PrecoPlanoFaixa? GetCurrentValidPrice(int healthPlanId, int ageRangeId, string tipoContratacao, string tipoCoparticipacao, DateTime date);
        
        /// <summary>
        /// Gets all active plan price ranges within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active plan price ranges</returns>
        IEnumerable<PrecoPlanoFaixa> GetActivePricesInPeriod(DateTime startDate, DateTime endDate);
    }
}