namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Coverage payload operations.
    /// Used for creating and updating Coverage instances.
    /// </summary>
    public class CoveragePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the coverage.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the coverage.
        /// For example: "Medical Consultations", "Laboratory Tests", "Surgeries".
        /// </summary>
        public string Name { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the description of the coverage.
        /// Detailed explanation of what is covered.
        /// </summary>
        public string? Description { get; set; }
        
        /// <summary>
        /// Gets or sets the type of coverage.
        /// Possible values: Outpatient, Hospital, Obstetric, Dental.
        /// </summary>
        public string CoverageType { get; set; } = string.Empty;
        
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