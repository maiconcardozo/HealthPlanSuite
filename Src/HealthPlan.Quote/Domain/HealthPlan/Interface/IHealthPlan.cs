using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IHealthPlan : IEntity
    {
        int HealthInsuranceOperatorId { get; set; }
        int PlanTypeId { get; set; }
        string Name { get; set; }
        string Coverage { get; set; }
        bool HasCoparticipation { get; set; }
    }
}