namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for AgeRange payload operations.
    /// Used for creating and updating AgeRange instances.
    /// </summary>
    public class AgeRangePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the age range.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the minimum age for this range (inclusive).
        /// </summary>
        public int MinAge { get; set; }
        
        /// <summary>
        /// Gets or sets the maximum age for this range (inclusive).
        /// </summary>
        public int MaxAge { get; set; }
        
        /// <summary>
        /// Gets or sets the description of the age range.
        /// For example: "0-18 anos", "19-23 anos", "24-28 anos", etc.
        /// </summary>
        public string Description { get; set; } = string.Empty;
        
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