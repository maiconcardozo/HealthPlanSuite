using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for Beneficiary business operations.
    /// Provides business logic layer for Beneficiary management.
    /// </summary>
    public interface IBeneficiaryService
    {
        IEnumerable<Beneficiary> GetAllActiveBeneficiaries();
        Beneficiary? GetById(int id);
        void AddBeneficiary(Beneficiary beneficiary);
        void UpdateBeneficiary(Beneficiary beneficiary);
        void DeleteBeneficiary(int id);
    }
}