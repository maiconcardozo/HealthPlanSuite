using HealthPlan.Login.Domain.Implementation;

namespace HealthPlan.Login.Domain.Interface;

public interface IHealthPlanRepository
{
    Task<IEnumerable<HealthPlanEntity>> GetAllAsync();
    Task<HealthPlanEntity?> GetByIdAsync(int id);
    Task<HealthPlanEntity> CreateAsync(HealthPlanEntity healthPlan);
    Task<HealthPlanEntity> UpdateAsync(HealthPlanEntity healthPlan);
    Task<bool> DeleteAsync(int id);
}