using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using HealthPlan.Quote.Services.HealthPlan.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class HealthPlanService : IHealthPlanService
    {
        private readonly IHealthPlanRepository _repository;

        public HealthPlanService(IHealthPlanRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetAll()
        {
            return _repository.GetAll();
        }

        public Domain.HealthPlan.Implementation.HealthPlan? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByOperatorId(int operatorId)
        {
            return _repository.GetByOperatorId(operatorId);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByPlanTypeId(int planTypeId)
        {
            return _repository.GetByPlanTypeId(planTypeId);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetWithCoverage()
        {
            return _repository.GetWithCoverage();
        }

        public Domain.HealthPlan.Implementation.HealthPlan Add(Domain.HealthPlan.Implementation.HealthPlan healthPlan)
        {
            return _repository.Add(healthPlan);
        }

        public void Update(Domain.HealthPlan.Implementation.HealthPlan healthPlan)
        {
            _repository.Update(healthPlan);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}