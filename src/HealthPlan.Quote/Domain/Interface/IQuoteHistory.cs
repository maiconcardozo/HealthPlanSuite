using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.Interface
{
    /// <summary>
    /// Represents the history of status changes for a quote.
    /// This interface tracks all status transitions and provides an audit trail for quotes.
    /// </summary>
    public interface IQuoteHistory : IEntity
    {
        /// <summary>
        /// Gets or sets the quote ID this history entry relates to.
        /// </summary>
        int QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the previous status of the quote.
        /// </summary>
        string? PreviousStatus { get; set; }

        /// <summary>
        /// Gets or sets the new status of the quote.
        /// </summary>
        string NewStatus { get; set; }

        /// <summary>
        /// Gets or sets the reason for the status change.
        /// </summary>
        string? Reason { get; set; }

        /// <summary>
        /// Gets or sets additional observations about the status change.
        /// </summary>
        string? Observations { get; set; }

        /// <summary>
        /// Gets or sets the date when the status change occurred.
        /// </summary>
        DateTime ChangeDate { get; set; }

        /// <summary>
        /// Gets or sets the user responsible for the status change.
        /// </summary>
        string ResponsibleUser { get; set; }
    }
}