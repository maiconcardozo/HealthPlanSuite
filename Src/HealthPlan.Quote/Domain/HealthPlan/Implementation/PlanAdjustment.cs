using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class PlanAdjustment : Entity, IPlanAdjustment
    {
        public int HealthPlanId { get; set; }
        public decimal Percentage { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string AdjustmentType { get; set; } = string.Empty;
        public string? Observation { get; set; }

        // Navigation properties
        public virtual HealthPlan HealthPlan { get; set; } = null!;
    }
}