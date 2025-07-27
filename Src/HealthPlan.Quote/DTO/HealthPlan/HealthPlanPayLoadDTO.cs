namespace HealthPlan.Quote.DTO.HealthPlan
{
    public class HealthPlanPayLoadDTO
    {
        public int HealthInsuranceOperatorId { get; set; }
        public int PlanTypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Coverage { get; set; } = string.Empty;
        public bool HasCoparticipation { get; set; }
    }
}