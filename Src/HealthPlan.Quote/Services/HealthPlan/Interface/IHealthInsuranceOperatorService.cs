using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IHealthInsuranceOperatorService
    {
        IEnumerable<HealthInsuranceOperator> GetAll();
        HealthInsuranceOperator? GetById(int id);
        HealthInsuranceOperator? GetByCNPJ(string cnpj);
        IEnumerable<HealthInsuranceOperator> GetByName(string name);
        HealthInsuranceOperator Add(HealthInsuranceOperator healthOperator);
        void Update(HealthInsuranceOperator healthOperator);
        void Delete(int id);
    }
}