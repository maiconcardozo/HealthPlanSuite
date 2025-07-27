using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IPlanType : IEntity
    {
        string Description { get; set; }
        string ANSRegulation { get; set; }
    }
}