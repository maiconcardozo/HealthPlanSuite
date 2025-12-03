using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Quote data access operations.
    /// Extends base repository functionality with Quote-specific methods.
    /// </summary>
    public interface IQuoteRepository : IEntityRepository<Domain.Entities.Quote>
    {
        /// <summary>
        /// Finds a quote by its quote number.
        /// </summary>
        /// <param name="quoteNumber">Quote number to search for</param>
        /// <returns>Quote if found, null otherwise</returns>
        Domain.Entities.Quote? GetByQuoteNumber(string quoteNumber);
        
        /// <summary>
        /// Retrieves all quotes for a specific beneficiary.
        /// </summary>
        /// <param name="beneficiaryId">Beneficiary ID</param>
        /// <returns>Collection of quotes for the beneficiary</returns>
        IEnumerable<Domain.Entities.Quote> GetByBeneficiaryId(int beneficiaryId);
        
        /// <summary>
        /// Retrieves all quotes from a specific company.
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>Collection of quotes from the company</returns>
        IEnumerable<Domain.Entities.Quote> GetByCompanyId(int companyId);
        
        /// <summary>
        /// Retrieves all quotes with a specific status.
        /// </summary>
        /// <param name="status">Quote status</param>
        /// <returns>Collection of quotes with the specified status</returns>
        IEnumerable<Domain.Entities.Quote> GetByStatus(string status);
        
        /// <summary>
        /// Retrieves all quotes that are still valid (ValidUntil > current date).
        /// </summary>
        /// <returns>Collection of valid quotes</returns>
        IEnumerable<Domain.Entities.Quote> GetValidQuotes();
        
        /// <summary>
        /// Retrieves all quotes that have expired (ValidUntil <= current date).
        /// </summary>
        /// <returns>Collection of expired quotes</returns>
        IEnumerable<Domain.Entities.Quote> GetExpiredQuotes();
        
        /// <summary>
        /// Checks if a quote number already exists.
        /// </summary>
        /// <param name="quoteNumber">Quote number to check</param>
        /// <returns>True if the quote number exists, false otherwise</returns>
        bool QuoteNumberExists(string quoteNumber);
    }
}