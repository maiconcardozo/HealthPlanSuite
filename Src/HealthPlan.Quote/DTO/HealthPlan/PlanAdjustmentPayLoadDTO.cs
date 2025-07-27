namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class PlanAdjustmentPayLoadDTO
    {
        public int HealthPlanId { get; set; }
        public decimal Percentage { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string AdjustmentType { get; set; } = string.Empty;
        public string? Observation { get; set; }
    }
}