using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IPlanCoverage : IEntity
    {
        int HealthPlanId { get; set; }
        int HealthEstablishmentId { get; set; }
    }
}