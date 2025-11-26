using MediatR;

namespace HealthPlan.Quote.Application.Queries.Quote
{
    /// <summary>
    /// Query to get a quote by its ID.
    /// </summary>
    public class GetQuoteByIdQuery : IRequest<GetQuoteByIdResponse?>
    {
        /// <summary>
        /// Gets or sets the quote ID to retrieve.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the GetQuoteByIdQuery class.
        /// </summary>
        /// <param name="id">The quote ID to retrieve</param>
        public GetQuoteByIdQuery(int id)
        {
            Id = id;
        }
    }

    /// <summary>
    /// Response for the GetQuoteByIdQuery.
    /// </summary>
    public class GetQuoteByIdResponse
    {
        /// <summary>
        /// Gets or sets the quote ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quote number.
        /// </summary>
        public string QuoteNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the company ID.
        /// </summary>
        public int IdCompany { get; set; }

        /// <summary>
        /// Gets or sets the beneficiary ID.
        /// </summary>
        public int IdBeneficiary { get; set; }

        /// <summary>
        /// Gets or sets the health plan ID.
        /// </summary>
        public int IdHealthPlan { get; set; }

        /// <summary>
        /// Gets or sets the age range ID.
        /// </summary>
        public int IdAgeRange { get; set; }

        /// <summary>
        /// Gets or sets the monthly premium amount.
        /// </summary>
        public decimal MonthlyPremium { get; set; }

        /// <summary>
        /// Gets or sets the status of the quote.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date until which the quote is valid.
        /// </summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// Gets or sets the date when the quote was generated.
        /// </summary>
        public DateTime QuoteDate { get; set; }

        /// <summary>
        /// Gets or sets the user who created the quote.
        /// </summary>
        public string CreatedBy { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional notes.
        /// </summary>
        public string? Notes { get; set; }
    }
}
