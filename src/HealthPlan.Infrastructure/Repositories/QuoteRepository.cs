using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Repositories;
using HealthPlan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Quote management operations.
    /// Provides concrete data access methods for Quote following the repository pattern.
    /// </summary>
    public class QuoteRepository : EntityRepository<Domain.Entities.Quote>, IQuoteRepository
    {
        /// <summary>
        /// Initializes a new instance of the QuoteRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public QuoteRepository(IApplicationContext context) : base(context)
        {
        }

        /// <summary>
        /// Finds a quote by its quote number.
        /// </summary>
        /// <param name="quoteNumber">Quote number to search for</param>
        /// <returns>Quote if found, null otherwise</returns>
        public Domain.Entities.Quote? GetByQuoteNumber(string quoteNumber)
        {
            return _context.Set<Domain.Entities.Quote>().FirstOrDefault(q => q.QuoteNumber == quoteNumber);
        }

        /// <summary>
        /// Retrieves all quotes for a specific beneficiary.
        /// </summary>
        /// <param name="beneficiaryId">Beneficiary ID</param>
        /// <returns>Collection of quotes for the beneficiary</returns>
        public IEnumerable<Domain.Entities.Quote> GetByBeneficiaryId(int beneficiaryId)
        {
            return _context.Set<Domain.Entities.Quote>()
                .Where(q => q.BeneficiaryId == beneficiaryId)
                .OrderByDescending(q => q.QuoteDate)
                .ToList();
        }

        /// <summary>
        /// Retrieves all quotes from a specific company.
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>Collection of quotes from the company</returns>
        public IEnumerable<Domain.Entities.Quote> GetByCompanyId(int companyId)
        {
            return _context.Set<Domain.Entities.Quote>()
                .Where(q => q.CompanyId == companyId)
                .OrderByDescending(q => q.QuoteDate)
                .ToList();
        }

        /// <summary>
        /// Retrieves all quotes with a specific status.
        /// </summary>
        /// <param name="status">Quote status</param>
        /// <returns>Collection of quotes with the specified status</returns>
        public IEnumerable<Domain.Entities.Quote> GetByStatus(string status)
        {
            return _context.Set<Domain.Entities.Quote>()
                .Where(q => q.Status == status)
                .OrderByDescending(q => q.QuoteDate)
                .ToList();
        }

        /// <summary>
        /// Retrieves all quotes that are still valid (ValidUntil > current date).
        /// </summary>
        /// <returns>Collection of valid quotes</returns>
        public IEnumerable<Domain.Entities.Quote> GetValidQuotes()
        {
            var currentDate = DateTime.UtcNow;
            return _context.Set<Domain.Entities.Quote>()
                .Where(q => q.ValidUntil > currentDate)
                .OrderByDescending(q => q.QuoteDate)
                .ToList();
        }

        /// <summary>
        /// Retrieves all quotes that have expired (ValidUntil <= current date).
        /// </summary>
        /// <returns>Collection of expired quotes</returns>
        public IEnumerable<Domain.Entities.Quote> GetExpiredQuotes()
        {
            var currentDate = DateTime.UtcNow;
            return _context.Set<Domain.Entities.Quote>()
                .Where(q => q.ValidUntil <= currentDate)
                .OrderByDescending(q => q.QuoteDate)
                .ToList();
        }

        /// <summary>
        /// Checks if a quote number already exists.
        /// </summary>
        /// <param name="quoteNumber">Quote number to check</param>
        /// <returns>True if the quote number exists, false otherwise</returns>
        public bool QuoteNumberExists(string quoteNumber)
        {
            return _context.Set<Domain.Entities.Quote>().Any(q => q.QuoteNumber == quoteNumber);
        }
    }
}