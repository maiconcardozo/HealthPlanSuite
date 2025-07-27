using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class PlanAdjustmentRepository : EntityRepository<PlanAdjustment>, IPlanAdjustmentRepository
    {
        public PlanAdjustmentRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PlanAdjustment> GetByHealthPlanId(int healthPlanId)
        {
            return Context.Set<PlanAdjustment>()
                .Include(x => x.HealthPlan)
                .Where(x => x.HealthPlanId == healthPlanId)
                .OrderByDescending(x => x.AdjustmentDate)
                .ToList();
        }

        public IEnumerable<PlanAdjustment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            return Context.Set<PlanAdjustment>()
                .Include(x => x.HealthPlan)
                .Where(x => x.AdjustmentDate >= startDate && x.AdjustmentDate <= endDate)
                .OrderByDescending(x => x.AdjustmentDate)
                .ToList();
        }

        public IEnumerable<PlanAdjustment> GetByAdjustmentType(string adjustmentType)
        {
            return Context.Set<PlanAdjustment>()
                .Include(x => x.HealthPlan)
                .Where(x => x.AdjustmentType == adjustmentType)
                .OrderByDescending(x => x.AdjustmentDate)
                .ToList();
        }
    }
}