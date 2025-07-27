namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class HealthPlanResponseDTO
    {
        public int Id { get; set; }
        public int HealthInsuranceOperatorId { get; set; }
        public int PlanTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Coverage { get; set; } = string.Empty;
        public bool HasCoparticipation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public string HealthInsuranceOperatorName { get; set; } = string.Empty;
        public string PlanTypeName { get; set; } = string.Empty;
    }
}