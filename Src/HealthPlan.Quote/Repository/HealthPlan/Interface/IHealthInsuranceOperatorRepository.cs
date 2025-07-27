using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IHealthInsuranceOperatorRepository : IEntityRepository<HealthInsuranceOperator>
    {
        HealthInsuranceOperator? GetByCNPJ(string cnpj);
        IEnumerable<HealthInsuranceOperator> GetByName(string name);
    }
}