using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Infrastructure.Repositories;
using HealthPlan.Infrastructure.Persistence;

namespace HealthPlan.Infrastructure.Repositories
{
    /// <summary>
    /// Repository implementation for Beneficiary management operations.
    /// Provides concrete data access methods for Beneficiary following the repository pattern.
    /// </summary>
    public class BeneficiaryRepository : EntityRepository<Beneficiary>, IBeneficiaryRepository
    {
        /// <summary>
        /// Initializes a new instance of the BeneficiaryRepository.
        /// </summary>
        /// <param name="context">Database context for data operations</param>
        public BeneficiaryRepository(IApplicationContext context) : base(context)
        {
        }
    }
}