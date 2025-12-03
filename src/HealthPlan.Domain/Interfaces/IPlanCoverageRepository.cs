using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for PlanCoverage data access operations.
    /// Extends base repository functionality with PlanCoverage-specific methods.
    /// </summary>
    public interface IPlanCoverageRepository : IEntityRepository<PlanCoverage>
    {
        /// <summary>
        /// Retrieves plan coverages by health plan ID.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <returns>Collection of plan coverages for the specified health plan</returns>
        IEnumerable<PlanCoverage> GetByHealthPlanId(int healthPlanId);
        
        /// <summary>
        /// Retrieves plan coverages by coverage ID.
        /// </summary>
        /// <param name="coverageId">Coverage ID</param>
        /// <returns>Collection of plan coverages for the specified coverage</returns>
        IEnumerable<PlanCoverage> GetByCoverageId(int coverageId);
        
        /// <summary>
        /// Retrieves included plan coverages.
        /// </summary>
        /// <returns>Collection of included plan coverages</returns>
        IEnumerable<PlanCoverage> GetIncludedCoverages();
        
        /// <summary>
        /// Retrieves plan coverages with premium value within a range.
        /// </summary>
        /// <param name="minValue">Minimum premium value</param>
        /// <param name="maxValue">Maximum premium value</param>
        /// <returns>Collection of plan coverages within the specified premium range</returns>
        IEnumerable<PlanCoverage> GetByPremiumRange(decimal minValue, decimal maxValue);
        
        /// <summary>
        /// Checks if a health plan and coverage combination already exists.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="coverageId">Coverage ID</param>
        /// <returns>True if the combination exists, false otherwise</returns>
        bool HealthPlanCoverageCombinationExists(int healthPlanId, int coverageId);
        
        /// <summary>
        /// Checks if a health plan and coverage combination exists for a different plan coverage (used during updates).
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="coverageId">Coverage ID</param>
        /// <param name="excludeId">PlanCoverage ID to exclude from the check</param>
        /// <returns>True if the combination exists for another plan coverage, false otherwise</returns>
        bool HealthPlanCoverageCombinationExistsForDifferentPlanCoverage(int healthPlanId, int coverageId, int excludeId);
    }
}