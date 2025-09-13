using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for HealthPlan business operations.
    /// Provides business logic layer for HealthPlan management.
    /// </summary>
    public interface IHealthPlanService
    {
        IEnumerable<Domain.Implementation.HealthPlan> GetAllActiveHealthPlans();
        Domain.Implementation.HealthPlan? GetById(int id);
        void AddHealthPlan(Domain.Implementation.HealthPlan healthPlan);
        void UpdateHealthPlan(Domain.Implementation.HealthPlan healthPlan);
        void DeleteHealthPlan(int id);
    }
}