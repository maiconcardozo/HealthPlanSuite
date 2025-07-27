namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class PriceTablePayLoadDTO
    {
        public int HealthPlanId { get; set; }
        public int AgeRangeId { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal? CoparticipationValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}