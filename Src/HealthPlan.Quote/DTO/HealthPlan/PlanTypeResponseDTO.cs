namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class PlanTypeResponseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ANSRegulation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}