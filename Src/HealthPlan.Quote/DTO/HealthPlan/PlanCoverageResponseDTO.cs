namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class PlanCoverageResponseDTO
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public int HealthEstablishmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public string HealthPlanName { get; set; } = string.Empty;
        public string HealthEstablishmentName { get; set; } = string.Empty;
        public string HealthEstablishmentType { get; set; } = string.Empty;
    }
}