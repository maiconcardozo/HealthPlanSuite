using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for HealthPlan business operations.
    /// Provides business logic layer for HealthPlan management.
    /// </summary>
    public interface IHealthPlanService
    {
        IEnumerable<Domain.Entities.HealthPlan> GetAllActiveHealthPlans();
        Domain.Entities.HealthPlan? GetById(int id);
        void AddHealthPlan(Domain.Entities.HealthPlan healthPlan);
        void UpdateHealthPlan(Domain.Entities.HealthPlan healthPlan);
        void DeleteHealthPlan(int id);
    }
}