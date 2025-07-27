namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class AgeRangeResponseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}