using HealthPlan.Quote.Constants;
using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;
using System.Linq.Expressions;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for Quote management operations.
    /// Provides business logic and data access coordination for Quote operations.
    /// </summary>
    public class QuoteService : IQuoteService
    {
        private readonly IQuoteRepository _quoteRepository;

        /// <summary>
        /// Initializes a new instance of the QuoteService.
        /// </summary>
        /// <param name="quoteRepository">Repository for quote data operations</param>
        public QuoteService(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        #region Query Operations

        /// <summary>
        /// Retrieves all quotes from the system.
        /// </summary>
        /// <returns>Collection of all quote entities</returns>
        public IEnumerable<Domain.Implementation.Quote> GetAllQuotes()
        {
            return _quoteRepository.GetAll().Where(q => q.IsActive);
        }

        /// <summary>
        /// Finds a quote by quote number.
        /// </summary>
        /// <param name="quoteNumber">The quote number to search for</param>
        /// <returns>Quote if found, null otherwise</returns>
        public Domain.Implementation.Quote? GetQuoteByNumber(string quoteNumber)
        {
            return _quoteRepository.GetByQuoteNumber(quoteNumber);
        }

        /// <summary>
        /// Retrieves a quote by its unique identifier.
        /// </summary>
        /// <param name="id">Quote ID</param>
        /// <returns>Quote if found, null otherwise</returns>
        public Domain.Implementation.Quote? GetById(int id)
        {
            return _quoteRepository.GetById(id);
        }

        /// <summary>
        /// Retrieves multiple quotes by their IDs.
        /// </summary>
        /// <param name="quoteIds">Collection of quote IDs</param>
        /// <returns>Collection of matching quote entities</returns>
        public IEnumerable<Domain.Implementation.Quote> GetQuotesByIds(IEnumerable<int> quoteIds)
        {
            var result = new List<Domain.Implementation.Quote>();
            foreach (var id in quoteIds)
            {
                var entity = _quoteRepository.GetById(id);
                if (entity != null)
                {
                    result.Add(entity);
                }
            }
            return result;
        }

        /// <summary>
        /// Retrieves quotes for a specific beneficiary.
        /// </summary>
        /// <param name="beneficiaryId">Beneficiary ID</param>
        /// <returns>Collection of quotes for the beneficiary</returns>
        public IEnumerable<Domain.Implementation.Quote> GetQuotesByBeneficiary(int beneficiaryId)
        {
            return _quoteRepository.GetByBeneficiaryId(beneficiaryId);
        }

        /// <summary>
        /// Retrieves quotes for a specific company.
        /// </summary>
        /// <param name="companyId">Company ID</param>
        /// <returns>Collection of quotes from the company</returns>
        public IEnumerable<Domain.Implementation.Quote> GetQuotesByCompany(int companyId)
        {
            return _quoteRepository.GetByCompanyId(companyId);
        }

        /// <summary>
        /// Retrieves quotes that match the specified predicate condition.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter quotes</param>
        /// <returns>Collection of matching quote entities</returns>
        public IEnumerable<Domain.Implementation.Quote> GetQuotes(Expression<Func<Domain.Implementation.Quote, bool>> predicate)
        {
            return _quoteRepository.Find(predicate);
        }

        /// <summary>
        /// Retrieves a single quote that matches the predicate, or null if none found.
        /// </summary>
        /// <param name="predicate">LINQ expression to filter quotes</param>
        /// <returns>Single matching quote or null</returns>
        public Domain.Implementation.Quote? GetSingleOrDefaultQuote(Expression<Func<Domain.Implementation.Quote, bool>> predicate)
        {
            return _quoteRepository.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Retrieves all active quotes.
        /// </summary>
        /// <returns>Collection of active quotes</returns>
        public IEnumerable<Domain.Implementation.Quote> GetAllActiveQuotes()
        {
            return _quoteRepository.GetAll().Where(q => q.IsActive);
        }

        /// <summary>
        /// Retrieves all pending quotes.
        /// </summary>
        /// <returns>Collection of pending quotes</returns>
        public IEnumerable<Domain.Implementation.Quote> GetAllPendingQuotes()
        {
            return _quoteRepository.GetByStatus("Pending");
        }

        #endregion

        #region Modification Operations

        /// <summary>
        /// Creates a new quote in the system.
        /// Generates quote number and sets audit fields.
        /// </summary>
        /// <param name="quote">Quote to create</param>
        public void AddQuote(Domain.Implementation.Quote quote)
        {
            // Generate unique quote number if not provided
            if (string.IsNullOrEmpty(quote.QuoteNumber))
            {
                quote.QuoteNumber = GenerateQuoteNumber();
            }

            // Set default values
            if (quote.QuoteDate == default)
            {
                quote.QuoteDate = DateTime.UtcNow;
            }

            if (string.IsNullOrEmpty(quote.Status))
            {
                quote.Status = "Pending";
            }

            // Set audit fields for tracking when and by whom the quote was created
            quote.DtCreated = DateTime.Now;
            // Use the CreatedBy value from the entity/DTO instead of a default value
            if (string.IsNullOrEmpty(quote.CreatedBy))
            {
                quote.CreatedBy = ApplicationConstants.DefaultCreatedByUser;
            }

            _quoteRepository.Add(quote);
        }

        /// <summary>
        /// Creates multiple quotes in a single transaction.
        /// </summary>
        /// <param name="quotes">Collection of quote entities to create</param>
        public void AddQuotes(IEnumerable<Domain.Implementation.Quote> quotes)
        {
            foreach (var quote in quotes)
            {
                // Set audit fields for each entity
                quote.DtCreated = DateTime.Now;
                if (string.IsNullOrEmpty(quote.CreatedBy))
                {
                    quote.CreatedBy = ApplicationConstants.DefaultCreatedByUser;
                }
                
                // Generate unique quote number if not provided
                if (string.IsNullOrEmpty(quote.QuoteNumber))
                {
                    quote.QuoteNumber = GenerateQuoteNumber();
                }

                // Set default values
                if (quote.QuoteDate == default)
                {
                    quote.QuoteDate = DateTime.UtcNow;
                }

                if (string.IsNullOrEmpty(quote.Status))
                {
                    quote.Status = "Pending";
                }
            }
            
            _quoteRepository.AddRange(quotes);
        }

        /// <summary>
        /// Updates an existing quote.
        /// </summary>
        /// <param name="quote">Quote with updated information</param>
        public void UpdateQuote(Domain.Implementation.Quote quote)
        {
            // Update audit fields for tracking modifications
            quote.DtUpdated = DateTime.Now;
            // Use the UpdatedBy value from the entity/DTO instead of a default value
            if (string.IsNullOrEmpty(quote.UpdatedBy))
            {
                quote.UpdatedBy = ApplicationConstants.DefaultCreatedByUser;
            }
            
            _quoteRepository.Update(quote);
        }

        /// <summary>
        /// Updates the status of a quote.
        /// </summary>
        /// <param name="quoteId">Quote ID</param>
        /// <param name="status">New status</param>
        public void UpdateQuoteStatus(int quoteId, string status)
        {
            var quote = _quoteRepository.GetById(quoteId);
            if (quote != null)
            {
                quote.Status = status;
                _quoteRepository.Update(quote);
            }
        }

        /// <summary>
        /// Deletes a quote.
        /// </summary>
        /// <param name="quote">Quote to delete</param>
        public void DeleteQuote(Domain.Implementation.Quote quote)
        {
            _quoteRepository.Remove(quote);
        }

        /// <summary>
        /// Deletes a quote by its ID.
        /// </summary>
        /// <param name="id">Quote ID to delete</param>
        public void DeleteQuote(int id)
        {
            var quote = _quoteRepository.GetById(id);
            if (quote != null)
            {
                _quoteRepository.Remove(quote);
            }
        }

        /// <summary>
        /// Deletes multiple quote entities.
        /// </summary>
        /// <param name="quotes">Collection of quote entities to delete</param>
        public void DeleteQuotes(IEnumerable<Domain.Implementation.Quote> quotes)
        {
            foreach (var quote in quotes)
            {
                _quoteRepository.Remove(quote);
            }
        }

        #endregion

        #region Business Logic

        /// <summary>
        /// Generates a unique quote number.
        /// </summary>
        /// <returns>Unique quote number</returns>
        public string GenerateQuoteNumber()
        {
            string quoteNumber;
            do
            {
                // Generate quote number with format: QT-YYYYMMDD-HHMMSS-XXXX
                var now = DateTime.UtcNow;
                var randomPart = new Random().Next(1000, 9999);
                quoteNumber = $"QT-{now:yyyyMMdd}-{now:HHmmss}-{randomPart}";
            } 
            while (_quoteRepository.QuoteNumberExists(quoteNumber));

            return quoteNumber;
        }

        /// <summary>
        /// Calculates premium based on age and health plan.
        /// </summary>
        /// <param name="healthPlanId">Health plan ID</param>
        /// <param name="ageRangeId">Age range ID</param>
        /// <returns>Calculated premium amount</returns>
        public decimal CalculatePremium(int healthPlanId, int ageRangeId)
        {
            // This is a simplified calculation
            // In a real system, this would involve complex business rules,
            // actuarial tables, and configuration data
            decimal basePremium = 100.00m; // Base premium amount
            
            // Age factor (this would normally come from a lookup table)
            decimal ageFactor = ageRangeId switch
            {
                1 => 0.5m,  // 0-18 years: 50% of base
                2 => 1.0m,  // 19-23 years: 100% of base
                3 => 1.2m,  // 24-28 years: 120% of base
                4 => 1.5m,  // 29-33 years: 150% of base
                5 => 1.8m,  // 34-38 years: 180% of base
                6 => 2.2m,  // 39-43 years: 220% of base
                7 => 2.8m,  // 44-48 years: 280% of base
                8 => 3.5m,  // 49-53 years: 350% of base
                9 => 4.2m,  // 54-58 years: 420% of base
                _ => 5.0m   // 59+ years: 500% of base
            };

            // Health plan factor (this would normally come from plan configuration)
            decimal planFactor = healthPlanId switch
            {
                1 => 1.0m,  // Basic plan
                2 => 1.5m,  // Standard plan
                3 => 2.0m,  // Premium plan
                _ => 1.0m   // Default
            };

            return basePremium * ageFactor * planFactor;
        }

        #endregion
    }
}