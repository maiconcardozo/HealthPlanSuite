using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for PrecoPlanoFaixa management operations.
    /// Provides concrete data access methods for PrecoPlanoFaixa following the repository pattern.
    /// </summary>
    public class PrecoPlanoFaixaRepository : EntityRepository<PrecoPlanoFaixa>, IPrecoPlanoFaixaRepository
    {
        /// <summary>
        /// Initializes a new instance of the PrecoPlanoFaixaRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public PrecoPlanoFaixaRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves plan price ranges for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of plan price ranges for the health plan</returns>
        public IEnumerable<PrecoPlanoFaixa> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<PrecoPlanoFaixa>()
                .Where(ppf => ppf.HealthPlanId == healthPlanId)
                .OrderBy(ppf => ppf.AgeRangeId)
                .ThenBy(ppf => ppf.TipoContratacao)
                .ThenBy(ppf => ppf.ValidadeInicio)
                .ToList();
        }

        /// <summary>
        /// Gets plan price ranges for a specific age range.
        /// </summary>
        /// <param name="ageRangeId">Age range ID</param>
        /// <returns>Collection of plan price ranges for the age range</returns>
        public IEnumerable<PrecoPlanoFaixa> GetByAgeRangeId(int ageRangeId)
        {
            return _context.Set<PrecoPlanoFaixa>()
                .Where(ppf => ppf.AgeRangeId == ageRangeId)
                .OrderBy(ppf => ppf.HealthPlanId)
                .ThenBy(ppf => ppf.TipoContratacao)
                .ThenBy(ppf => ppf.ValidadeInicio)
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
        public PrecoPlanoFaixa? GetCurrentValidPrice(int healthPlanId, int ageRangeId, string tipoContratacao, string tipoCoparticipacao, DateTime date)
        {
            return _context.Set<PrecoPlanoFaixa>()
                .FirstOrDefault(ppf => ppf.HealthPlanId == healthPlanId 
                    && ppf.AgeRangeId == ageRangeId
                    && ppf.TipoContratacao == tipoContratacao
                    && ppf.TipoCoparticipacao == tipoCoparticipacao
                    && ppf.ValidadeInicio <= date 
                    && ppf.ValidadeFim >= date 
                    && ppf.IsActive);
        }

        /// <summary>
        /// Gets all active plan price ranges within a date range.
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <returns>Collection of active plan price ranges</returns>
        public IEnumerable<PrecoPlanoFaixa> GetActivePricesInPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.Set<PrecoPlanoFaixa>()
                .Where(ppf => ppf.ValidadeInicio <= endDate 
                    && ppf.ValidadeFim >= startDate 
                    && ppf.IsActive)
                .OrderBy(ppf => ppf.HealthPlanId)
                .ThenBy(ppf => ppf.AgeRangeId)
                .ThenBy(ppf => ppf.ValidadeInicio)
                .ToList();
        }
    }
}