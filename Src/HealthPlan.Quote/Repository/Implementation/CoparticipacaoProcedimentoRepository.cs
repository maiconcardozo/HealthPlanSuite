using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for CoparticipacaoProcedimento management operations.
    /// Provides concrete data access methods for CoparticipacaoProcedimento following the repository pattern.
    /// </summary>
    public class CoparticipacaoProcedimentoRepository : EntityRepository<CoparticipacaoProcedimento>, ICoparticipacaoProcedimentoRepository
    {
        /// <summary>
        /// Initializes a new instance of the CoparticipacaoProcedimentoRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public CoparticipacaoProcedimentoRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves co-participation procedures for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of co-participation procedures for the health plan</returns>
        public IEnumerable<CoparticipacaoProcedimento> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<CoparticipacaoProcedimento>()
                .Where(cp => cp.HealthPlanId == healthPlanId)
                .OrderBy(cp => cp.TipoCoparticipacao)
                .ThenBy(cp => cp.Procedimento)
                .ToList();
        }

        /// <summary>
        /// Gets co-participation procedures by type.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="tipoCoparticipacao">Type of co-participation</param>
        /// <returns>Collection of co-participation procedures of the specified type</returns>
        public IEnumerable<CoparticipacaoProcedimento> GetByType(int healthPlanId, string tipoCoparticipacao)
        {
            return _context.Set<CoparticipacaoProcedimento>()
                .Where(cp => cp.HealthPlanId == healthPlanId && cp.TipoCoparticipacao == tipoCoparticipacao)
                .OrderBy(cp => cp.Procedimento)
                .ToList();
        }

        /// <summary>
        /// Gets co-participation for a specific procedure.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="procedimento">Procedure name</param>
        /// <returns>Co-participation procedure if found, null otherwise</returns>
        public CoparticipacaoProcedimento? GetByProcedure(int healthPlanId, string procedimento)
        {
            return _context.Set<CoparticipacaoProcedimento>()
                .FirstOrDefault(cp => cp.HealthPlanId == healthPlanId 
                    && cp.Procedimento == procedimento 
                    && cp.IsActive);
        }
    }
}