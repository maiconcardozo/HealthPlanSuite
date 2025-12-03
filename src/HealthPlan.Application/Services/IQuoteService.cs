using System.Linq.Expressions;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for Quote management operations.
    /// Provides comprehensive Quote CRUD operations following service layer patterns.
    /// </summary>
    public interface IQuoteService
    {
        #region Query Operations
        
        /// <summary>
        /// Retrieves all quotes from the system.
        /// </summary>
        /// <returns>Collection of all quote entities</returns>
        IEnumerable<Domain.Entities.Quote> GetAllQuotes();
        
        /// <summary>
        /// Finds a quote by quote number.
        /// </summary>
        /// <param name="quoteNumber">The quote number to search for</param>
        /// <returns>Quote if found, null otherwise</returns>
        Domain.Entities.Quote? GetQuoteByNumber(string quoteNumber);
        
        /// <summary>
        /// Retrieves a quote by its unique identifier.
        /// </summary>
        /// <param name="id">Quote ID</param>
        /// <returns>Quote if found, null otherwise</returns>
        Domain.Entities.Quote? GetById(int id);
        
        /// <summary>
        /// Retrieves multiple quotes by their IDs.
        /// </summary>
        /// <param name="quoteIds">Collection of quote IDs</param>
        /// <returns>Collection of matching quote entities</returns>
        IEnumerable<Domain.Entities.Quote> GetQuotesByIds(IEnumerable<int> quoteIds);
        
        /// <summary>
        /// Retrieves quotes for a specific beneficiary.
        /// </summary>
        /// <param name="beneficiaryId">Beneficiary ID</param>
        /// <returns>Collection of quotes for the beneficiary</returns>
        IEnumerable<Domain.Entities.Quote> GetQuotesByBeneficiary(int beneficiaryId);
        
        /// <summary>
        /// Retrieves quotes for a specific company.
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>Collection of quotes from the company</returns>
        IEnumerable<Domain.Entities.Quote> GetQuotesByCompany(int companyId);
        
        /// <summary>
        /// Retrieves quotes that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter quotes</param>
        /// <returns>Collection of matching quote entities</returns>
        IEnumerable<Domain.Entities.Quote> GetQuotes(Expression<Func<Domain.Entities.Quote, bool>> predicate);
        
        /// <summary>
        /// Retrieves a single quote that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter quotes</param>
        /// <returns>Single matching quote or null</returns>
        /// <exception cref="InvalidOperationException">Thrown when multiple quotes match the predicate</exception>
        Domain.Entities.Quote? GetSingleOrDefaultQuote(Expression<Func<Domain.Entities.Quote, bool>> predicate);
        
        /// <summary>
        /// Retrieves all active quotes.
        /// </summary>
        /// <returns>Collection of active quotes</returns>
        IEnumerable<Domain.Entities.Quote> GetAllActiveQuotes();
        
        /// <summary>
        /// Retrieves all pending quotes.
        /// </summary>
        /// <returns>Collection of pending quotes</returns>
        IEnumerable<Domain.Entities.Quote> GetAllPendingQuotes();
        
        #endregion
        
        #region Modification Operations
        
        /// <summary>
        /// Creates a new quote in the system.
        /// Generates quote number and sets audit fields.
        /// </summary>
        /// <param name="quote">Quote to create</param>
        void AddQuote(Domain.Entities.Quote quote);
        
        /// <summary>
        /// Creates multiple quotes in a single transaction.
        /// </summary>
        /// <param name="quotes">Collection of quote entities to create</param>
        void AddQuotes(IEnumerable<Domain.Entities.Quote> quotes);
        
        /// <summary>
        /// Updates an existing quote.
        /// </summary>
        /// <param name="quote">Quote with updated information</param>
        void UpdateQuote(Domain.Entities.Quote quote);
        
        /// <summary>
        /// Updates the status of a quote.
        /// </summary>
        /// <param name="quoteId">Quote ID</param>
        /// <param name="status">New status</param>
        void UpdateQuoteStatus(int quoteId, string status);
        
        /// <summary>
        /// Deletes a quote.
        /// </summary>
        /// <param name="quote">Quote to delete</param>
        void DeleteQuote(Domain.Entities.Quote quote);
        
        /// <summary>
        /// Deletes a quote by its ID.
        /// </summary>
        /// <param name="id">Quote ID to delete</param>
        void DeleteQuote(int id);
        
        /// <summary>
        /// Deletes multiple quote entities.
        /// </summary>
        /// <param name="quotes">Collection of quote entities to delete</param>
        void DeleteQuotes(IEnumerable<Domain.Entities.Quote> quotes);
        
        #endregion
        
        #region Business Logic
        
        /// <summary>
        /// Generates a unique quote number.
        /// </summary>
        /// <returns>Unique quote number</returns>
        string GenerateQuoteNumber();
        
        /// <summary>
        /// Calculates premium based on age and health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="ageRangeId">Age range ID</param>
        /// <returns>Calculated premium amount</returns>
        decimal CalculatePremium(int healthPlanId, int ageRangeId);
        
        #endregion
    }
}