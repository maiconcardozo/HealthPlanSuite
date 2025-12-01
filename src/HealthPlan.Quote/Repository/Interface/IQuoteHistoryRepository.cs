using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for QuoteHistory data access operations.
    /// Extends base repository functionality with QuoteHistory-specific methods.
    /// </summary>
    public interface IQuoteHistoryRepository : IEntityRepository<QuoteHistory>
    {
        /// <summary>
        /// Retrieves quote histories by quote ID.
        /// </summary>
        /// <param name="quoteId">Quote ID</param>
        /// <returns>Collection of quote histories for the specified quote</returns>
        IEnumerable<QuoteHistory> GetByQuoteId(int quoteId);
        
        /// <summary>
        /// Retrieves quote histories by status.
        /// </summary>
        /// <param name="status">Status to filter by</param>
        /// <returns>Collection of quote histories with the specified status</returns>
        IEnumerable<QuoteHistory> GetByStatus(string status);
        
        /// <summary>
        /// Retrieves quote histories by responsible user.
        /// </summary>
        /// <param name="responsibleUser">Responsible user to filter by</param>
        /// <returns>Collection of quote histories by the specified user</returns>
        IEnumerable<QuoteHistory> GetByResponsibleUser(string responsibleUser);
        
        /// <summary>
        /// Retrieves quote histories within a date range.
        /// </summary>
        /// <param name="startDate">Start date of the range</param>
        /// <param name="endDate">End date of the range</param>
        /// <returns>Collection of quote histories within the specified date range</returns>
        IEnumerable<QuoteHistory> GetByDateRange(DateTime startDate, DateTime endDate);
        
        /// <summary>
        /// Gets the latest quote history for a specific quote.
        /// </summary>
        /// <param name="quoteId">Quote ID</param>
        /// <returns>Latest quote history for the specified quote</returns>
        QuoteHistory? GetLatestQuoteHistory(int quoteId);
        
        /// <summary>
        /// Checks if a status is valid.
        /// </summary>
        /// <param name="status">Status to validate</param>
        /// <returns>True if status is valid, false otherwise</returns>
        bool IsValidStatus(string status);
    }
}