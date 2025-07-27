using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IPlanCoverageService
    {
        IEnumerable<PlanCoverage> GetAll();
        PlanCoverage? GetById(int id);
        IEnumerable<PlanCoverage> GetByHealthPlanId(int healthPlanId);
        IEnumerable<PlanCoverage> GetByHealthEstablishmentId(int healthEstablishmentId);
        bool ExistsCoverage(int healthPlanId, int healthEstablishmentId);
        PlanCoverage Add(PlanCoverage planCoverage);
        void Update(PlanCoverage planCoverage);
        void Delete(int id);
    }
}