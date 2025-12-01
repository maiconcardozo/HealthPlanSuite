using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for Coverage API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class CoverageRoutes
    {
        /// <summary>
        /// Route for getting all coverages.
        /// </summary>
        public const string GetCoverages = "coverages";
        
        /// <summary>
        /// Route for getting a coverage by ID.
        /// </summary>
        public const string GetCoverageById = "{id}";
        
        /// <summary>
        /// Route for getting coverages by type.
        /// </summary>
        public const string GetCoveragesByType = "type/{coverageType}";
        
        /// <summary>
        /// Route for adding a new coverage.
        /// </summary>
        public const string AddCoverage = "";
        
        /// <summary>
        /// Route for updating an existing coverage.
        /// </summary>
        public const string UpdateCoverage = "";
        
        /// <summary>
        /// Route for deleting a coverage.
        /// </summary>
        public const string DeleteCoverage = "{id}";
    }
}