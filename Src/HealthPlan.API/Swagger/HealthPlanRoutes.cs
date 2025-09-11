using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for HealthPlan API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class HealthPlanRoutes
    {
        /// <summary>
        /// Route for getting all health plans.
        /// </summary>
        public const string GetHealthPlans = "healthplans";
        
        /// <summary>
        /// Route for getting a health plan by ID.
        /// </summary>
        public const string GetHealthPlanById = "{id}";
        
        /// <summary>
        /// Route for getting health plans by company.
        /// </summary>
        public const string GetHealthPlansByCompany = "company/{companyId}";
        
        /// <summary>
        /// Route for getting health plan by code.
        /// </summary>
        public const string GetHealthPlanByCode = "code/{code}";
        
        /// <summary>
        /// Route for adding a new health plan.
        /// </summary>
        public const string AddHealthPlan = "";
        
        /// <summary>
        /// Route for updating an existing health plan.
        /// </summary>
        public const string UpdateHealthPlan = "{id}";
        
        /// <summary>
        /// Route for deleting a health plan.
        /// </summary>
        public const string DeleteHealthPlan = "{id}";
    }
}