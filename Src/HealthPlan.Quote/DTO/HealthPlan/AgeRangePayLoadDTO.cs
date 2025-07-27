namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class AgeRangePayLoadDTO
    {
        public string Description { get; set; } = string.Empty;
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
    }
}