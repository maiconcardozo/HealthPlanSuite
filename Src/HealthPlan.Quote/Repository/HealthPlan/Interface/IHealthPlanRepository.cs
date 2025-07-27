using Foundation.Base.Repository.Interface;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IHealthPlanRepository : IEntityRepository<Domain.HealthPlan.Implementation.HealthPlan>
    {
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByOperatorId(int operatorId);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByPlanTypeId(int planTypeId);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByName(string name);
        IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetWithCoverage();
    }
}