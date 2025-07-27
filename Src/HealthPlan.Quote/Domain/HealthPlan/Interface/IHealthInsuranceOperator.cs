using Foundation.Base.Domain.Interface;

namespace HealthPlan.Quote.Domain.HealthPlan.Interface
{
    public interface IHealthInsuranceOperator : IEntity
    {
        string Name { get; set; }
        string CNPJ { get; set; }
        string Website { get; set; }
        string Phone { get; set; }
    }
}