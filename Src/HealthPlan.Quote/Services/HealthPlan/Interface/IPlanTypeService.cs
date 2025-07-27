using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IPlanTypeService
    {
        IEnumerable<PlanType> GetAll();
        PlanType? GetById(int id);
        IEnumerable<PlanType> GetByDescription(string description);
        PlanType Add(PlanType planType);
        void Update(PlanType planType);
        void Delete(int id);
    }
}