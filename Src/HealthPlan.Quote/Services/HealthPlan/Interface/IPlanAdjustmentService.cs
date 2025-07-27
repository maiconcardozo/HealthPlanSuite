using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IPlanAdjustmentService
    {
        IEnumerable<PlanAdjustment> GetAll();
        PlanAdjustment? GetById(int id);
        IEnumerable<PlanAdjustment> GetByHealthPlanId(int healthPlanId);
        IEnumerable<PlanAdjustment> GetByDateRange(DateTime startDate, DateTime endDate);
        IEnumerable<PlanAdjustment> GetByAdjustmentType(string adjustmentType);
        PlanAdjustment Add(PlanAdjustment planAdjustment);
        void Update(PlanAdjustment planAdjustment);
        void Delete(int id);
    }
}