using HealthPlan.Domain.Interfaces;
using HealthPlan.Domain.Entities;

namespace HealthPlan.Domain.Interfaces
{
    /// <summary>
    /// Repository interface for Beneficiary data access operations.
    /// Extends base repository functionality with Beneficiary-specific methods.
    /// </summary>
    public interface IBeneficiaryRepository : IEntityRepository<Beneficiary>
    {
    }
}