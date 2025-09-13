using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Infrastructure.Interface;

namespace HealthPlan.Quote.Repository.Implementation
{
    /// <summary>
    /// Repository implementation for Beneficiary management operations.
    /// Provides concrete data access methods for Beneficiary following the repository pattern.
    /// </summary>
    public class BeneficiaryRepository : EntityRepository<Beneficiary>, IBeneficiaryRepository
    {
        private readonly IApplicationContext _context;

        /// <summary>
        /// Initializes a new instance of the BeneficiaryRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public BeneficiaryRepository(IApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}