using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class PlanTypeRepository : EntityRepository<PlanType>, IPlanTypeRepository
    {
        public PlanTypeRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PlanType> GetByDescription(string description)
        {
            return Context.Set<PlanType>()
                .Where(x => x.Description.Contains(description))
                .ToList();
        }
    }
}