namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for AdhesionFee payload operations.
    /// Used for creating and updating AdhesionFee instances.
    /// </summary>
    public class AdhesionFeePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the adhesion fee.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the health plan ID that this adhesion fee belongs to.
        /// References the HealthPlan entity.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the adhesion fee value.
        /// The monetary amount charged as an adhesion fee for the health plan.
        /// </summary>
        public decimal Value { get; set; }
        
        /// <summary>
        /// Gets or sets the start date of validity for this adhesion fee.
        /// The date from which this fee becomes effective.
        /// </summary>
        public DateTime ValidityStart { get; set; }
        
        /// <summary>
        /// Gets or sets the end date of validity for this adhesion fee.
        /// The date until which this fee remains effective.
        /// </summary>
        public DateTime ValidityEnd { get; set; }
        
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