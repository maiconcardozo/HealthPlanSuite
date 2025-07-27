namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class PlanAdjustmentResponseDTO
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public decimal Percentage { get; set; }
        public DateTime AdjustmentDate { get; set; }
        public string AdjustmentType { get; set; } = string.Empty;
        public string? Observation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public string HealthPlanName { get; set; } = string.Empty;
    }
}