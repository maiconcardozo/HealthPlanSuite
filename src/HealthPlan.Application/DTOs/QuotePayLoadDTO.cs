namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Quote payload operations.
    /// Used for creating and updating Quote instances.
    /// </summary>
    public class QuotePayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quote.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the company ID that is providing the quote.
        /// </summary>
        public int IdCompany { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// </summary>
        public int IdBeneficiary { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// </summary>
        public int IdHealthPlan { get; set; }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// </summary>
        public int IdAgeRange { get; set; }
        
        // DEPRECATED properties for backward compatibility
        /// <summary>
        /// Gets or sets the company ID that is providing the quote.
        /// DEPRECATED: Use IdCompany instead.
        /// </summary>
        [Obsolete("Use IdCompany instead")]
        public int CompanyId 
        { 
            get => IdCompany; 
            set => IdCompany = value; 
        }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// DEPRECATED: Use IdBeneficiary instead.
        /// </summary>
        [Obsolete("Use IdBeneficiary instead")]
        public int BeneficiaryId 
        { 
            get => IdBeneficiary; 
            set => IdBeneficiary = value; 
        }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// DEPRECATED: Use IdHealthPlan instead.
        /// </summary>
        [Obsolete("Use IdHealthPlan instead")]
        public int HealthPlanId 
        { 
            get => IdHealthPlan; 
            set => IdHealthPlan = value; 
        }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// DEPRECATED: Use IdAgeRange instead.
        /// </summary>
        [Obsolete("Use IdAgeRange instead")]
        public int AgeRangeId 
        { 
            get => IdAgeRange; 
            set => IdAgeRange = value; 
        }
        
        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// </summary>
        public DateTime ValidUntil { get; set; }
        
        /// <summary>
        /// Gets or sets the monthly premium amount for the health plan.
        /// </summary>
        public decimal MonthlyPremium { get; set; }
        
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