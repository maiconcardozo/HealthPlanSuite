namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class PriceTableResponseDTO
    {
        public int Id { get; set; }
        public int HealthPlanId { get; set; }
        public int AgeRangeId { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal? CoparticipationValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation properties
        public string HealthPlanName { get; set; } = string.Empty;
        public string AgeRangeDescription { get; set; } = string.Empty;
    }
}