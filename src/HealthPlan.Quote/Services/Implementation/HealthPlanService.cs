using HealthPlan.Quote.Domain.Implementation;
using HealthPlan.Quote.Repository.Interface;
using HealthPlan.Quote.Services.Interface;

namespace HealthPlan.Quote.Services.Implementation
{
    /// <summary>
    /// Service implementation for HealthPlan business operations.
    /// </summary>
    public class HealthPlanService : IHealthPlanService
    {
        private readonly IHealthPlanRepository _healthPlanRepository;

        public HealthPlanService(IHealthPlanRepository healthPlanRepository)
        {
            _healthPlanRepository = healthPlanRepository;
        }

        public IEnumerable<Domain.Implementation.HealthPlan> GetAllActiveHealthPlans()
        {
            return _healthPlanRepository.Find(hp => hp.IsActive);
        }

        public Domain.Implementation.HealthPlan? GetById(int id)
        {
            return _healthPlanRepository.GetById(id);
        }

        public void AddHealthPlan(Domain.Implementation.HealthPlan healthPlan)
        {
            _healthPlanRepository.Add(healthPlan);
        }

        public void UpdateHealthPlan(Domain.Implementation.HealthPlan healthPlan)
        {
            _healthPlanRepository.Update(healthPlan);
        }

        public void DeleteHealthPlan(int id)
        {
            _healthPlanRepository.Remove(id);
        }
    }
}