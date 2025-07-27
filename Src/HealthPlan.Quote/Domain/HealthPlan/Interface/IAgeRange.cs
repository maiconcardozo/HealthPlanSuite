using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IAgeRange : IEntity
    {
        string Description { get; set; }
        int MinAge { get; set; }
        int MaxAge { get; set; }
    }
}