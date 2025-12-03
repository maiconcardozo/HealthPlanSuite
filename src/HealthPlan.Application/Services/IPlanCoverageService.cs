using System.Linq.Expressions;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for PlanCoverage management operations.
    /// Provides comprehensive PlanCoverage CRUD operations following service layer patterns.
    /// </summary>
    public interface IPlanCoverageService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all plan coverages from the system.
        /// </summary>
        /// <returns>Collection of all plan coverage entities</returns>
        IEnumerable<PlanCoverage> GetAllPlanCoverages();
        
        /// <summary>
        /// Retrieves a plan coverage by its unique identifier.
        /// </summary>
        /// <param name="id">PlanCoverage ID</param>
        /// <returns>PlanCoverage if found, null otherwise</returns>
        PlanCoverage? GetById(int id);
        
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
        /// Retrieves plan coverages that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter plan coverages</param>
        /// <returns>Collection of matching plan coverage entities</returns>
        IEnumerable<PlanCoverage> GetPlanCoverages(Expression<Func<PlanCoverage, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single plan coverage that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter plan coverages</param>
        /// <returns>Single matching plan coverage or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple plan coverages match the predicate</exception>
        PlanCoverage? GetSingleOrDefaultPlanCoverage(Expression<Func<PlanCoverage, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active plan coverages.
        /// </summary>
        /// <returns>Collection of active plan coverages</returns>
        IEnumerable<PlanCoverage> GetAllActivePlanCoverages();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new plan coverage in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="planCoverage">PlanCoverage to create</param>
        void AddPlanCoverage(PlanCoverage planCoverage);
        
        /// <summary>
        /// Creates multiple plan coverages in a single transaction.
        /// </summary>
        /// <param name="planCoverages">Collection of plan coverage entities to create</param>
        void AddPlanCoverages(IEnumerable<PlanCoverage> planCoverages);
        
        /// <summary>
        /// Updates an existing plan coverage.
        /// </summary>
        /// <param name="planCoverage">PlanCoverage with updated information</param>
        void UpdatePlanCoverage(PlanCoverage planCoverage);
        
        /// <summary>
        /// Deletes a plan coverage.
        /// </summary>
        /// <param name="planCoverage">PlanCoverage to delete</param>
        void DeletePlanCoverage(PlanCoverage planCoverage);
        
        /// <summary>
        /// Deletes a plan coverage by its ID.
        /// </summary>
        /// <param name="id">PlanCoverage ID to delete</param>
        void DeletePlanCoverage(int id);
        
        /// <summary>
        /// Deletes multiple plan coverage entities.
        /// </summary>
        /// <param name="planCoverages">Collection of plan coverage entities to delete</param>
        void DeletePlanCoverages(IEnumerable<PlanCoverage> planCoverages);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Validates if a health plan and coverage combination is unique.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="coverageId">Coverage ID</param>
        /// <returns>True if combination is unique, false otherwise</returns>
        bool IsHealthPlanCoverageCombinationUnique(int healthPlanId, int coverageId);
        
        /// <summary>
        /// Validates if a health plan and coverage combination is unique for updates (excludes current entity).
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="coverageId">Coverage ID</param>
        /// <param name="excludeId">PlanCoverage ID to exclude from validation</param>
        /// <returns>True if combination is unique, false otherwise</returns>
        bool IsHealthPlanCoverageCombinationUniqueForUpdate(int healthPlanId, int coverageId, int excludeId);
        
        #endregion
    }
}