namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for ProcedureCoparticipation payload operations.
    /// Used for creating and updating ProcedureCoparticipation instances.
    /// </summary>
    public class ProcedureCoparticipationPayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the procedure coparticipation.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the health plan ID that this co-participation applies to.
        /// References the HealthPlan entity.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the type of co-participation.
        /// Possible values: "Parcial" or "Total".
        /// </summary>
        public string CoparticipationType { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the procedure name or description.
        /// The medical procedure this co-participation applies to.
        /// </summary>
        public string Procedure { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the co-participation value.
        /// The monetary amount or percentage of co-participation.
        /// </summary>
        public decimal Value { get; set; }
        
        /// <summary>
        /// Gets or sets the limit for this co-participation.
        /// Maximum amount or frequency limit for this co-participation.
        /// </summary>
        public decimal? Limit { get; set; }
        
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