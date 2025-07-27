using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class PriceTable : Entity, IPriceTable
    {
        public int HealthPlanId { get; set; }
        public int AgeRangeId { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal? CoparticipationValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation properties
        public virtual HealthPlan HealthPlan { get; set; } = null!;
        public virtual AgeRange AgeRange { get; set; } = null!;
    }
}