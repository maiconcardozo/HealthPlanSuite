namespace HealthPlan.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for QuoteHistory payload operations.
    /// Used for creating and updating QuoteHistory instances.
    /// </summary>
    public class QuoteHistoryPayLoadDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the quote history.
        /// Used for update operations.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the quote ID this history entry relates to.
        /// </summary>
        public int QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the previous status of the quote.
        /// </summary>
        public string? PreviousStatus { get; set; }

        /// <summary>
        /// Gets or sets the new status of the quote.
        /// </summary>
        public string NewStatus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reason for the status change.
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets additional observations about the status change.
        /// </summary>
        public string? Observations { get; set; }

        /// <summary>
        /// Gets or sets the date when the status change occurred.
        /// </summary>
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the user responsible for the status change.
        /// </summary>
        public string ResponsibleUser { get; set; } = string.Empty;

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