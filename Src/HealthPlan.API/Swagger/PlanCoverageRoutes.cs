using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for PlanCoverage API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class PlanCoverageRoutes
    {
        /// <summary>
        /// Route for getting all plan coverages.
        /// </summary>
        public const string GetPlanCoverages = "plan-coverages";
        
        /// <summary>
        /// Route for getting a plan coverage by ID.
        /// </summary>
        public const string GetPlanCoverageById = "{id}";
        
        /// <summary>
        /// Route for getting plan coverages by health plan ID.
        /// </summary>
        public const string GetPlanCoveragesByHealthPlanId = "health-plan/{healthPlanId}";
        
        /// <summary>
        /// Route for adding a new plan coverage.
        /// </summary>
        public const string AddPlanCoverage = "";
        
        /// <summary>
        /// Route for updating an existing plan coverage.
        /// </summary>
        public const string UpdatePlanCoverage = "";
        
        /// <summary>
        /// Route for deleting a plan coverage.
        /// </summary>
        public const string DeletePlanCoverage = "{id}";
    }
}