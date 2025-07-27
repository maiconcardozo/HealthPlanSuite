using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IPlanTypeRepository : IEntityRepository<PlanType>
    {
        IEnumerable<PlanType> GetByDescription(string description);
    }
}