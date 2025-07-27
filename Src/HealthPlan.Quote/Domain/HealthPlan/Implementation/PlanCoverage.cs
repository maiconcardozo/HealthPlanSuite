using Foundation.Base.Domain.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Implementation
{
    public class PlanCoverage : Entity, IPlanCoverage
    {
        public int HealthPlanId { get; set; }
        public int HealthEstablishmentId { get; set; }

        // Navigation properties
        public virtual HealthPlan HealthPlan { get; set; } = null!;
        public virtual HealthEstablishment HealthEstablishment { get; set; } = null!;
    }
}