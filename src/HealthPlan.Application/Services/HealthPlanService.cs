using HealthPlan.Domain.Entities;
using HealthPlan.Domain.Interfaces;
using HealthPlan.Application.Services;

namespace HealthPlan.Application.Services
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

        public IEnumerable<Domain.Entities.HealthPlan> GetAllActiveHealthPlans()
        {
            return _healthPlanRepository.Find(hp => hp.IsActive);
        }

        public Domain.Entities.HealthPlan? GetById(int id)
        {
            return _healthPlanRepository.GetById(id);
        }

        public void AddHealthPlan(Domain.Entities.HealthPlan healthPlan)
        {
            _healthPlanRepository.Add(healthPlan);
        }

        public void UpdateHealthPlan(Domain.Entities.HealthPlan healthPlan)
        {
            _healthPlanRepository.Update(healthPlan);
        }

        public void DeleteHealthPlan(int id)
        {
            _healthPlanRepository.Remove(id);
        }
    }
}