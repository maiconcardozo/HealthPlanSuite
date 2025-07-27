using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IHealthEstablishment : IEntity
    {
        string Name { get; set; }
        string Type { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
    }
}