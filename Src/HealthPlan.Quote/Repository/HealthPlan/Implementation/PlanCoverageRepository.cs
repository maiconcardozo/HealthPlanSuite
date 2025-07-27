using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class PlanCoverageRepository : EntityRepository<PlanCoverage>, IPlanCoverageRepository
    {
        public PlanCoverageRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PlanCoverage> GetByHealthPlanId(int healthPlanId)
        {
            return Context.Set<PlanCoverage>()
                .Include(x => x.HealthPlan)
                .Include(x => x.HealthEstablishment)
                .Where(x => x.HealthPlanId == healthPlanId)
                .ToList();
        }

        public IEnumerable<PlanCoverage> GetByHealthEstablishmentId(int healthEstablishmentId)
        {
            return Context.Set<PlanCoverage>()
                .Include(x => x.HealthPlan)
                .Include(x => x.HealthEstablishment)
                .Where(x => x.HealthEstablishmentId == healthEstablishmentId)
                .ToList();
        }

        public bool ExistsCoverage(int healthPlanId, int healthEstablishmentId)
        {
            return Context.Set<PlanCoverage>()
                .Any(x => x.HealthPlanId == healthPlanId && x.HealthEstablishmentId == healthEstablishmentId);
        }
    }
}