using HealthPlan.API.Resource;

namespace HealthPlan.API.Swagger
{
    /// <summary>
    /// Route constants for AcceptanceRule API endpoints.
    /// Uses resource files for localization and consistency.
    /// </summary>
    public static class AcceptanceRuleRoutes
    {
        /// <summary>
        /// Route for getting all acceptance rules.
        /// </summary>
        public const string GetAcceptanceRules = "acceptance-rules";
        
        /// <summary>
        /// Route for getting an acceptance rule by ID.
        /// </summary>
        public const string GetAcceptanceRuleById = "{id}";
        
        /// <summary>
        /// Route for getting acceptance rules by health plan ID.
        /// </summary>
        public const string GetAcceptanceRulesByHealthPlanId = "health-plan/{healthPlanId}";
        
        /// <summary>
        /// Route for adding a new acceptance rule.
        /// </summary>
        public const string AddAcceptanceRule = "";
        
        /// <summary>
        /// Route for updating an existing acceptance rule.
        /// </summary>
        public const string UpdateAcceptanceRule = "{id}";
        
        /// <summary>
        /// Route for deleting an acceptance rule.
        /// </summary>
        public const string DeleteAcceptanceRule = "{id}";
    }
}