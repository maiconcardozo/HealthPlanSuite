using HealthPlan.Quote.Domain.Interface;
using Foundation.Base.Domain.Implementation;

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
        /// Gets or sets the company ID that is providing the quote.
        /// References the Company entity.
        /// </summary>
        public int CompanyId { get; set; }
        
        /// <summary>
        /// Gets or sets the beneficiary ID for whom the quote is being generated.
        /// References the Beneficiary entity.
        /// </summary>
        public int BeneficiaryId { get; set; }
        
        /// <summary>
        /// Gets or sets the health plan ID being quoted.
        /// References the HealthPlan entity.
        /// </summary>
        public int HealthPlanId { get; set; }
        
        /// <summary>
        /// Gets or sets the unique quote number.
        /// Must be unique across the system.
        /// </summary>
        public string QuoteNumber { get; set; } = string.Empty;
        
        /// <summary>
        /// Gets or sets the date when the quote was generated.
        /// Defaults to current UTC time.
        /// </summary>
        public DateTime QuoteDate { get; set; } = DateTime.UtcNow;
        
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
        /// References the AgeRange entity.
        /// </summary>
        public int AgeRangeId { get; set; }
        
        /// <summary>
        /// Gets or sets the status of the quote.
        /// Possible values: Pending, Approved, Rejected, Expired.
        /// </summary>
        public string Status { get; set; } = "Pending";
        
        /// <summary>
        /// Gets or sets additional notes or comments about the quote.
        /// </summary>
        public string? Notes { get; set; }
    }
}