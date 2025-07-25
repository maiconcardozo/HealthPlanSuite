using HealthPlan.Login.Domain.Implementation;

namespace HealthPlan.Login.Domain.Interface;

public interface IBeneficiaryRepository
{
    Task<IEnumerable<Beneficiary>> GetAllAsync();
    Task<Beneficiary?> GetByIdAsync(int id);
    Task<IEnumerable<Beneficiary>> GetByHealthPlanIdAsync(int healthPlanId);
    Task<Beneficiary> CreateAsync(Beneficiary beneficiary);
    Task<Beneficiary> UpdateAsync(Beneficiary beneficiary);
    Task<bool> DeleteAsync(int id);
}