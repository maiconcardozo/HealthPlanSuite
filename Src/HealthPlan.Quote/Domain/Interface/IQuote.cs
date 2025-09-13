using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents a health plan quote for a specific beneficiary.
    /// Quotes contain pricing and coverage information for health plan proposals.
    /// </summary>
    public interface IQuote : IEntity
    {
        /// <summary>
        /// Gets or sets the company ID that is providing the quote.
        /// </summary>
        int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// </summary>
        int BeneficiaryId { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// </summary>
        int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the unique quote number.
        /// </summary>
        string QuoteNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the date when the quote was generated.
        /// </summary>
        DateTime QuoteDate { get; set; }
        
        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// </summary>
        DateTime ValidUntil { get; set; }
        
        /// <summary>
        /// Gets or sets the monthly premium amount for the health plan.
        /// </summary>
        decimal MonthlyPremium { get; set; }
        
        /// <summary>
        /// Gets or sets the age range ID used for premium calculation.
        /// </summary>
        int AgeRangeId { get; set; }
        
        /// <summary>
        /// Gets or sets the status of the quote.
        /// </summary>
        string Status { get; set; }
        
        /// <summary>
        /// Gets or sets additional notes or comments about the quote.
        /// </summary>
        string? Notes { get; set; }
    }
}
