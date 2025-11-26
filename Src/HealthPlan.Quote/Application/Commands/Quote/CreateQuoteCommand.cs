using MediatR;
using HealthPlan.Quote.Application.Behaviors;

namespace HealthPlan.Quote.Application.Commands.Quote
{
    /// <summary>
    /// Command to create a new quote.
    /// Implements ITransactionalRequest to ensure transaction wrapping.
    /// </summary>
    public class CreateQuoteCommand : IRequest<CreateQuoteResponse>, ITransactionalRequest
    {
        /// <summary>
        /// Gets or sets the company ID for the quote.
        /// </summary>
        public int IdCompany { get; set; }

        /// <summary>
        /// Gets or sets the beneficiary ID for the quote.
        /// </summary>
        public int IdBeneficiary { get; set; }

        /// <summary>
        /// Gets or sets the health plan ID for the quote.
        /// </summary>
        public int IdHealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the age range ID for the quote.
        /// </summary>
        public int IdAgeRange { get; set; }

        /// <summary>
        /// Gets or sets the monthly premium amount.
        /// </summary>
        public decimal MonthlyPremium { get; set; }

        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// </summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// Gets or sets the user who created the quote.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional notes for the quote.
        /// </summary>
        public string? Notes { get; set; }
    }

    /// <summary>
    /// Response for the CreateQuoteCommand.
    /// </summary>
    public class CreateQuoteResponse
    {
        /// <summary>
        /// Gets or sets the ID of the created quote.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quote number.
        /// </summary>
        public string QuoteNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the status of the quote.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the monthly premium amount.
        /// </summary>
        public decimal MonthlyPremium { get; set; }

        /// <summary>
        /// Gets or sets the quote date.
        /// </summary>
        public DateTime QuoteDate { get; set; }
    }
}
