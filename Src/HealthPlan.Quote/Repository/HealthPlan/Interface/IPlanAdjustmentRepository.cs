using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IPlanAdjustmentRepository : IEntityRepository<PlanAdjustment>
    {
        IEnumerable<PlanAdjustment> GetByHealthPlanId(int healthPlanId);
        IEnumerable<PlanAdjustment> GetByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<PlanAdjustment> GetByAdjustmentType(string adjustmentType);
    }
}