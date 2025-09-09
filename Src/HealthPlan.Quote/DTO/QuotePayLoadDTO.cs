namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Quote payload operations.
    /// Used for creating and updating Quote instances.
    /// </summary>
    public class QuotePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the company ID that is providing the quote.
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// </summary>
        public int BeneficiaryId { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// </summary>
        public DateTime ValidUntil { get; set; }
        
        /// <summary>
        /// Gets or sets the monthly premium amount for the health plan.
        /// </summary>
        public decimal MonthlyPremium { get; set; }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// </summary>
        public int AgeRangeId { get; set; }
        
        /// <summary>
        /// Gets or sets additional notes or comments about the quote.
        /// </summary>
        public string? Notes { get; set; }
        
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