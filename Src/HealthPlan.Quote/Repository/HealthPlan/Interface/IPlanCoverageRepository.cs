using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IPlanCoverageRepository : IEntityRepository<PlanCoverage>
    {
        IEnumerable<PlanCoverage> GetByHealthPlanId(int healthPlanId);
        IEnumerable<PlanCoverage> GetByHealthEstablishmentId(int healthEstablishmentId);
        bool ExistsCoverage(int healthPlanId, int healthEstablishmentId);
    }
}