using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IHealthInsuranceOperatorService
    {
        IEnumerable<HealthInsuranceOperator> GetAll();
        HealthInsuranceOperator? GetById(int id);
        HealthInsuranceOperator? GetByCNPJ(string cnpj);
        IEnumerable<HealthInsuranceOperator> GetByName(string name);
        HealthInsuranceOperator Add(HealthInsuranceOperator operator);
        void Update(HealthInsuranceOperator operator);
        void Delete(int id);
    }
}