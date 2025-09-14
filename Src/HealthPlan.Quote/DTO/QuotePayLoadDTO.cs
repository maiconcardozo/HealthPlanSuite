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
        public int IdEmpresa { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// </summary>
        public int IdBeneficiario { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// </summary>
        public int IdPlanoSaude { get; set; }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// </summary>
        public int IdFaixaEtaria { get; set; }
        
        // DEPRECATED properties for backward compatibility
        /// <summary>
        /// Gets or sets the company ID that is providing the quote.
        /// DEPRECATED: Use IdEmpresa instead.
        /// </summary>
        [Obsolete("Use IdEmpresa instead")]
        public int CompanyId 
        { 
            get => IdEmpresa; 
            set => IdEmpresa = value; 
        }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// DEPRECATED: Use IdBeneficiario instead.
        /// </summary>
        [Obsolete("Use IdBeneficiario instead")]
        public int BeneficiaryId 
        { 
            get => IdBeneficiario; 
            set => IdBeneficiario = value; 
        }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// DEPRECATED: Use IdPlanoSaude instead.
        /// </summary>
        [Obsolete("Use IdPlanoSaude instead")]
        public int HealthPlanId 
        { 
            get => IdPlanoSaude; 
            set => IdPlanoSaude = value; 
        }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// DEPRECATED: Use IdFaixaEtaria instead.
        /// </summary>
        [Obsolete("Use IdFaixaEtaria instead")]
        public int AgeRangeId 
        { 
            get => IdFaixaEtaria; 
            set => IdFaixaEtaria = value; 
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