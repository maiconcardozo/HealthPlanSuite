using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for ProcedureCoparticipation management operations.
    /// Provides concrete data access methods for ProcedureCoparticipation following the repository pattern.
    /// </summary>
    public class ProcedureCoparticipationRepository : EntityRepository<ProcedureCoparticipation>, IProcedureCoparticipationRepository
    {
        /// <summary>
        /// Initializes a new instance of the ProcedureCoparticipationRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public ProcedureCoparticipationRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves co-participation procedures for a specific health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of co-participation procedures for the health plan</returns>
        public IEnumerable<ProcedureCoparticipation> GetByHealthPlanId(int healthPlanId)
        {
            return _context.Set<ProcedureCoparticipation>()
                .Where(cp => cp.HealthPlanId == healthPlanId)
                .OrderBy(cp => cp.CoparticipationType)
                .ThenBy(cp => cp.Procedure)
                .ToList();
        }

        /// <summary>
        /// Gets co-participation procedures by type.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="tipoCoparticipacao">Type of co-participation</param>
        /// <returns>Collection of co-participation procedures of the specified type</returns>
        public IEnumerable<ProcedureCoparticipation> GetByType(int healthPlanId, string tipoCoparticipacao)
        {
            return _context.Set<ProcedureCoparticipation>()
                .Where(cp => cp.HealthPlanId == healthPlanId && cp.CoparticipationType == tipoCoparticipacao)
                .OrderBy(cp => cp.Procedure)
                .ToList();
        }

        /// <summary>
        /// Gets co-participation for a specific procedure.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="procedimento">Procedure name</param>
        /// <returns>Co-participation procedure if found, null otherwise</returns>
        public ProcedureCoparticipation? GetByProcedure(int healthPlanId, string procedimento)
        {
            return _context.Set<ProcedureCoparticipation>()
                .FirstOrDefault(cp => cp.HealthPlanId == healthPlanId 
                    && cp.Procedure == procedimento 
                    && cp.IsActive);
        }
    }
}