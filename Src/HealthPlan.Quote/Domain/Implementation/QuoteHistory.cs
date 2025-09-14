using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.Interface;

namespace HealthPlan.Quote.Domain.Implementation
{
    /// <summary>
    /// Represents the history of status changes for a quote.
    /// This entity tracks all status transitions and provides an audit trail for quotes.
    /// Inherits from Entity base class providing audit fields and implements IQuoteHistory interface.
    /// </summary>
    public class QuoteHistory : Entity, IQuoteHistory
    {
        /// <summary>
        /// Gets or sets the quote ID this history entry relates to.
        /// References the Quote entity.
        /// Maps to SQL column: CotacaoId
        /// </summary>
        public int QuoteId { get; set; }

        /// <summary>
        /// Gets or sets the quote this history entry relates to.
        /// Navigation property for QuoteId foreign key.
        /// </summary>
        public Quote? Quote { get; set; }

        /// <summary>
        /// Gets or sets the previous status of the quote.
        /// Maps to SQL column: StatusAnterior
        /// </summary>
        public string? PreviousStatus { get; set; }

        /// <summary>
        /// Gets or sets the new status of the quote.
        /// Maps to SQL column: StatusNovo
        /// </summary>
        public string NewStatus { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the reason for the status change.
        /// Maps to SQL column: Motivo
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// Gets or sets additional observations about the status change.
        /// Maps to SQL column: Observacoes
        /// </summary>
        public string? Observations { get; set; }

        /// <summary>
        /// Gets or sets the date when the status change occurred.
        /// Maps to SQL column: DataMudanca
        /// </summary>
        public DateTime ChangeDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the user responsible for the status change.
        /// Maps to SQL column: UsuarioResponsavel
        /// </summary>
        public string ResponsibleUser { get; set; } = string.Empty;
    }
}