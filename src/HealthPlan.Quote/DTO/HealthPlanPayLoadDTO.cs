namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for HealthPlan payload operations.
    /// Used for creating and updating HealthPlan instances.
    /// </summary>
    public class HealthPlanPayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the health plan.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company ID that offers this health plan.
        /// References the Company entity.
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the name of the health plan.
        /// For example: "Basic Plan", "Executive Plan", "Premium Plan".
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the unique code of the health plan.
        /// Must be unique across the system.
        /// </summary>
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the description of the health plan.
        /// Detailed explanation of the plan benefits and coverage.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of the health plan.
        /// Possible values: Individual, Family, Corporate.
        /// </summary>
        public string PlanType { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who created this entity.
        /// Used for audit trail purposes.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who last updated this entity.
        /// Used for audit trail purposes during updates.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}