using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IPlanAdjustment : IEntity
    {
        int HealthPlanId { get; set; }
        decimal Percentage { get; set; }
        DateTime AdjustmentDate { get; set; }
        string AdjustmentType { get; set; }
        string? Observation { get; set; }
    }
}