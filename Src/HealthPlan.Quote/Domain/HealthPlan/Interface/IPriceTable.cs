using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IPriceTable : IEntity
    {
        int HealthPlanId { get; set; }
        int AgeRangeId { get; set; }
        decimal MonthlyFee { get; set; }
        decimal? CoparticipationValue { get; set; }
        DateTime StartDate { get; set; }
        DateTime? EndDate { get; set; }
    }
}