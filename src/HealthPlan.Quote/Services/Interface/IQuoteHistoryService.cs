using System.Linq.Expressions;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for QuoteHistory management operations.
    /// Provides comprehensive QuoteHistory CRUD operations following service layer patterns.
    /// </summary>
    public interface IQuoteHistoryService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all quote histories from the system.
        /// </summary>
        /// <returns>Collection of all quote history entities</returns>
        IEnumerable<QuoteHistory> GetAllQuoteHistories();
        
        /// <summary>
        /// Retrieves a quote history by its unique identifier.
        /// </summary>
        /// <param name="id">QuoteHistory ID</param>
        /// <returns>QuoteHistory if found, null otherwise</returns>
        QuoteHistory? GetById(int id);
        
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
        /// Retrieves quote histories that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter quote histories</param>
        /// <returns>Collection of matching quote history entities</returns>
        IEnumerable<QuoteHistory> GetQuoteHistories(Expression<Func<QuoteHistory, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single quote history that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter quote histories</param>
        /// <returns>Single matching quote history or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple quote histories match the predicate</exception>
        QuoteHistory? GetSingleOrDefaultQuoteHistory(Expression<Func<QuoteHistory, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active quote histories.
        /// </summary>
        /// <returns>Collection of active quote histories</returns>
        IEnumerable<QuoteHistory> GetAllActiveQuoteHistories();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new quote history in the system.
        /// Sets audit fields and validates business rules.
        /// </summary>
        /// <param name="quoteHistory">QuoteHistory to create</param>
        void AddQuoteHistory(QuoteHistory quoteHistory);
        
        /// <summary>
        /// Creates multiple quote histories in a single transaction.
        /// </summary>
        /// <param name="quoteHistories">Collection of quote history entities to create</param>
        void AddQuoteHistories(IEnumerable<QuoteHistory> quoteHistories);
        
        /// <summary>
        /// Updates an existing quote history.
        /// </summary>
        /// <param name="quoteHistory">QuoteHistory with updated information</param>
        void UpdateQuoteHistory(QuoteHistory quoteHistory);
        
        /// <summary>
        /// Deletes a quote history.
        /// </summary>
        /// <param name="quoteHistory">QuoteHistory to delete</param>
        void DeleteQuoteHistory(QuoteHistory quoteHistory);
        
        /// <summary>
        /// Deletes a quote history by its ID.
        /// </summary>
        /// <param name="id">QuoteHistory ID to delete</param>
        void DeleteQuoteHistory(int id);
        
        /// <summary>
        /// Deletes multiple quote history entities.
        /// </summary>
        /// <param name="quoteHistories">Collection of quote history entities to delete</param>
        void DeleteQuoteHistories(IEnumerable<QuoteHistory> quoteHistories);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Validates if a status is valid.
        /// </summary>
        /// <param name="status">Status to validate</param>
        /// <returns>True if status is valid, false otherwise</returns>
        bool IsValidStatus(string status);
        
        /// <summary>
        /// Gets the latest quote history for a specific quote.
        /// </summary>
        /// <param name="quoteId">Quote ID</param>
        /// <returns>Latest quote history for the specified quote</returns>
        QuoteHistory? GetLatestQuoteHistory(int quoteId);
        
        #endregion
    }
}