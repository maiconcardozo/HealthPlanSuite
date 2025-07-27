using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class HealthPlanRepository : EntityRepository<Domain.HealthPlan.Implementation.HealthPlan>, IHealthPlanRepository
    {
        public HealthPlanRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByOperatorId(int operatorId)
        {
            return Context.Set<Domain.HealthPlan.Implementation.HealthPlan>()
                .Include(x => x.HealthInsuranceOperator)
                .Include(x => x.PlanType)
                .Where(x => x.HealthInsuranceOperatorId == operatorId)
                .ToList();
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByPlanTypeId(int planTypeId)
        {
            return Context.Set<Domain.HealthPlan.Implementation.HealthPlan>()
                .Include(x => x.HealthInsuranceOperator)
                .Include(x => x.PlanType)
                .Where(x => x.PlanTypeId == planTypeId)
                .ToList();
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByName(string name)
        {
            return Context.Set<Domain.HealthPlan.Implementation.HealthPlan>()
                .Include(x => x.HealthInsuranceOperator)
                .Include(x => x.PlanType)
                .Where(x => x.Name.Contains(name))
                .ToList();
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetWithCoverage()
        {
            return Context.Set<Domain.HealthPlan.Implementation.HealthPlan>()
                .Include(x => x.HealthInsuranceOperator)
                .Include(x => x.PlanType)
                .Include(x => x.PlanCoverages)
                    .ThenInclude(pc => pc.HealthEstablishment)
                .ToList();
        }
    }
}