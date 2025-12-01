namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for Quote response operations.
    /// Used for returning Quote data to API consumers.
    /// </summary>
    public class QuoteResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote.
        /// </summary>
        public int IdQuote { get; set; }
        
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
        /// Gets or sets the unique identifier of the quote.
        /// DEPRECATED: Use IdQuote instead.
        /// </summary>
        [Obsolete("Use IdQuote instead")]
        public int Id 
        { 
            get => IdQuote; 
            set => IdQuote = value; 
        }
        
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
        /// Gets or sets the unique quote number.
        /// </summary>
        public string QuoteNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the date when the quote was generated.
        /// </summary>
        public DateTime QuoteDate { get; set; }
        
        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// </summary>
        public DateTime ValidUntil { get; set; }
        
        /// <summary>
        /// Gets or sets the monthly premium amount for the health plan.
        /// </summary>
        public decimal MonthlyPremium { get; set; }
        
        /// <summary>
        /// Gets or sets the status of the quote.
        /// </summary>
        public string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets additional notes or comments about the quote.
        /// </summary>
        public string? Notes { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when this entity was created.
        /// </summary>
        public DateTime DtCreated { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when this entity was deleted (soft delete).
        /// Null if the entity is still active.
        /// </summary>
        public DateTime? DtDeleted { get; set; }
        
        /// <summary>
        /// Gets or sets the date and time when this entity was last updated.
        /// Null if the entity has never been updated.
        /// </summary>
        public DateTime? DtUpdated { get; set; }
        
        /// <summary>
        /// Gets or sets the user who created this entity.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the user who last updated this entity.
        /// Null if the entity has never been updated.
        /// </summary>
        public string? UpdatedBy { get; set; }
        
        /// <summary>
        /// Gets or sets the user who deleted this entity.
        /// Null if the entity is still active.
        /// </summary>
        public string? DeletedBy { get; set; }
    }
}