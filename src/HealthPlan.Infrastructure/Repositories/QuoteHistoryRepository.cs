using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Persistence;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for QuoteHistory management operations.
    /// </summary>
    public class QuoteHistoryRepository : EntityRepository<QuoteHistory>, IQuoteHistoryRepository
    {
        private static readonly string[] ValidStatuses = { "pending", "approved", "rejected", "cancelled", "in_review", "completed" };

        public QuoteHistoryRepository(IApplicationContext context) : base(context)
        {
        }

        public IEnumerable<QuoteHistory> GetByQuoteId(int quoteId)
        {
            return _context.Set<QuoteHistory>()
                .Where(qh => qh.QuoteId == quoteId && qh.IsActive)
                .OrderByDescending(qh => qh.ChangeDate)
                .ToList();
        }

        public IEnumerable<QuoteHistory> GetByStatus(string status)
        {
            return !IsValidStatus(status)
                ? Enumerable.Empty<QuoteHistory>()
                : _context.Set<QuoteHistory>()
                    .Where(qh => qh.NewStatus == status && qh.IsActive)
                    .ToList();
        }

        public IEnumerable<QuoteHistory> GetByResponsibleUser(string responsibleUser)
        {
            return _context.Set<QuoteHistory>()
                .Where(qh => qh.ResponsibleUser == responsibleUser && qh.IsActive)
                .ToList();
        }

        public IEnumerable<QuoteHistory> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return _context.Set<QuoteHistory>()
                .Where(qh => qh.ChangeDate >= startDate && qh.ChangeDate <= endDate && qh.IsActive)
                .OrderByDescending(qh => qh.ChangeDate)
                .ToList();
        }

        public QuoteHistory? GetLatestQuoteHistory(int quoteId)
        {
            return _context.Set<QuoteHistory>()
                .Where(qh => qh.QuoteId == quoteId && qh.IsActive)
                .OrderByDescending(qh => qh.ChangeDate)
                .FirstOrDefault();
        }

        public bool IsValidStatus(string status)
        {
            return ValidStatuses.Contains(status.ToLowerInvariant());
        }
    }
}
