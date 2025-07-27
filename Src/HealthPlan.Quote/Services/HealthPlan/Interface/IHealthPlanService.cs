namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IHealthPlanService
    {
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetAll();
        Domain.HealthPlan.Implementation.HealthPlan? GetById(int id);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByOperatorId(int operatorId);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByPlanTypeId(int planTypeId);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByName(string name);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetWithCoverage();
        Domain.HealthPlan.Implementation.HealthPlan Add(Domain.HealthPlan.Implementation.HealthPlan healthPlan);
        void Update(Domain.HealthPlan.Implementation.HealthPlan healthPlan);
        void Delete(int id);
    }
}