using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents a health plan quote implementation for a specific beneficiary.
    /// Quotes contain pricing and coverage information for health plan proposals.
    /// Inherits from Entity base class providing audit fields and implements IQuote interface.
    /// </summary>
    public class Quote : Entity, IQuote
    {
        /// <summary>
        /// Gets or sets the quote ID.
        /// Maps to SQL column: IdCotacao
        /// </summary>
        public int IdCotacao 
        { 
            get => Id; 
            set => Id = value; 
        }

        /// <summary>
        /// Gets or sets the company ID that is providing the quote.
        /// References the Company entity.
        /// Maps to SQL column: IdEmpresa
        /// </summary>
        public int IdEmpresa { get; set; }
        
        /// <summary>
        /// Gets or sets the company that is providing the quote.
        /// Navigation property for IdEmpresa foreign key.
        /// </summary>
        public Company? Company { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// References the Beneficiary entity.
        /// Maps to SQL column: IdBeneficiario
        /// </summary>
        public int IdBeneficiario { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary for whom the quote is being generated.
        /// Navigation property for IdBeneficiario foreign key.
        /// </summary>
        public Beneficiary? Beneficiary { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// References the HealthPlan entity.
        /// Maps to SQL column: IdPlanoSaude
        /// </summary>
        public int IdPlanoSaude { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan being quoted.
        /// Navigation property for IdPlanoSaude foreign key.
        /// </summary>
        public HealthPlan? HealthPlan { get; set; }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// References the AgeRange entity.
        /// Maps to SQL column: IdFaixaEtaria
        /// </summary>
        public int IdFaixaEtaria { get; set; }
        
        /// <summary>
        /// Gets or sets the age range used for premium calculation.
        /// Navigation property for IdFaixaEtaria foreign key.
        /// </summary>
        public AgeRange? AgeRange { get; set; }
        
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
        /// Gets or sets the unique quote number.
        /// Must be unique across the system.
        /// Maps to SQL column: NumeroCotacao
        /// </summary>
        public string QuoteNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the date when the quote was generated.
        /// Maps to SQL column: DataCotacao
        /// </summary>
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// Maps to SQL column: DataVencimento
        /// </summary>
        public DateTime ValidUntil { get; set; }
        
        /// <summary>
        /// Gets or sets the calculated premium amount for the health plan.
        /// Maps to SQL column: PremioCalculado
        /// </summary>
        public decimal CalculatedPremium { get; set; }

        /// <summary>
        /// Gets or sets the total value of the quote.
        /// Maps to SQL column: ValorTotal
        /// </summary>
        public decimal TotalValue { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage applied.
        /// Maps to SQL column: PercentualDesconto
        /// </summary>
        public decimal DiscountPercentage { get; set; } = 0.00m;

        /// <summary>
        /// Gets or sets the discount value in currency.
        /// Maps to SQL column: ValorDesconto
        /// </summary>
        public decimal DiscountValue { get; set; } = 0.00m;
        
        /// <summary>
        /// Gets or sets the status of the quote.
        /// Possible values: Pendente, Aprovada, Rejeitada, Expirada, Contratada.
        /// Maps to SQL column: Status
        /// </summary>
        public string Status { get; set; } = "Pendente";

        /// <summary>
        /// Gets or sets the reason for rejection (if applicable).
        /// Maps to SQL column: MotivoRejeicao
        /// </summary>
        public string? RejectionReason { get; set; }

        /// <summary>
        /// Gets or sets additional notes or comments about the quote.
        /// Maps to SQL column: Observacoes
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets the calculated age of the beneficiary at quote time.
        /// Maps to SQL column: IdadeCalculada
        /// </summary>
        public int CalculatedAge { get; set; }

        /// <summary>
        /// Gets or sets the validity period in days.
        /// Maps to SQL column: ValidadeDias
        /// </summary>
        public int ValidityDays { get; set; } = 30;

        /// <summary>
        /// Gets or sets the collection of quote history.
        /// Navigation property for the relationship with QuoteHistory.
        /// </summary>
        public ICollection<QuoteHistory>? QuoteHistories { get; set; }

        // Deprecated properties for backward compatibility
        /// <summary>
        /// Gets or sets the monthly premium amount for the health plan.
        /// DEPRECATED: Use CalculatedPremium instead.
        /// </summary>
        [Obsolete("Use CalculatedPremium instead")]
        public decimal MonthlyPremium 
        { 
            get => CalculatedPremium; 
            set => CalculatedPremium = value; 
        }
    }
}
