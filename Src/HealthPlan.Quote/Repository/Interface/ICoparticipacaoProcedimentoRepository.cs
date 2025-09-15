using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for CoparticipacaoProcedimento data access operations.
    /// Extends base repository functionality with CoparticipacaoProcedimento-specific methods.
    /// </summary>
    public interface ICoparticipacaoProcedimentoRepository : IEntityRepository<CoparticipacaoProcedimento>
    {
        /// <summary>
        /// Retrieves co-participation procedures for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of co-participation procedures for the health plan</returns>
        IEnumerable<CoparticipacaoProcedimento> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Gets co-participation procedures by type.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="tipoCoparticipacao">Type of co-participation</param>
        /// <returns>Collection of co-participation procedures of the specified type</returns>
        IEnumerable<CoparticipacaoProcedimento> GetByType(int healthPlanId, string tipoCoparticipacao);
        
        /// <summary>
        /// Gets co-participation for a specific procedure.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="procedimento">Procedure name</param>
        /// <returns>Co-participation procedure if found, null otherwise</returns>
        CoparticipacaoProcedimento? GetByProcedure(int healthPlanId, string procedimento);
    }
}