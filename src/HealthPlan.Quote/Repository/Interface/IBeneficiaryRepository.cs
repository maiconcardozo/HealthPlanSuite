using HealthPlan.Quote.Repository.Base;
using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Repository.Interface
{
    /// <summary>
    /// Repository interface for Beneficiary data access operations.
    /// Extends base repository functionality with Beneficiary-specific methods.
    /// </summary>
    public interface IBeneficiaryRepository : IEntityRepository<Beneficiary>
    {
    }
}