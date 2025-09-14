namespace HealthPlan.Quote.DTO
{
    /// <summary>
    /// Data Transfer Object for QuoteHistory response operations.
    /// Used for returning QuoteHistory data to API consumers.
    /// </summary>
    public class QuoteHistoryResponseDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quote history.
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