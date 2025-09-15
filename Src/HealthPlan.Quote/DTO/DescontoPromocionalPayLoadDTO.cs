namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for DescontoPromocional payload operations.
    /// Used for creating and updating DescontoPromocional instances.
    /// </summary>
    public class DescontoPromocionalPayLoadDTO
    {
        /// <summary>
        /// Gets or sets the health plan ID that this promotional discount applies to.
        /// References the HealthPlan entity.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the discount percentage.
        /// The percentage value of the promotional discount (e.g., 10 for 10%).
        /// </summary>
        public decimal PercentualDesconto { get; set; }
        
        /// <summary>
        /// Gets or sets the start date of validity for this promotional discount.
        /// The date from which this discount becomes effective.
        /// </summary>
        public DateTime ValidadeInicio { get; set; }
        
        /// <summary>
        /// Gets or sets the end date of validity for this promotional discount.
        /// The date until which this discount remains effective.
        /// </summary>
        public DateTime ValidadeFim { get; set; }
        
        /// <summary>
        /// Gets or sets the observation or description for this promotional discount.
        /// Additional details about the discount terms and conditions.
        /// </summary>
        public string? Observacao { get; set; }
        
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