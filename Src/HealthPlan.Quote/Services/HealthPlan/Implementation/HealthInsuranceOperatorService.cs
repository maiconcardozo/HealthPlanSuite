using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using HealthPlan.Quote.Services.HealthPlan.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class HealthInsuranceOperatorService : IHealthInsuranceOperatorService
    {
        private readonly IHealthInsuranceOperatorRepository _repository;

        public HealthInsuranceOperatorService(IHealthInsuranceOperatorRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<HealthInsuranceOperator> GetAll()
        {
            return _repository.GetAll();
        }

        public HealthInsuranceOperator? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public HealthInsuranceOperator? GetByCNPJ(string cnpj)
        {
            return _repository.GetByCNPJ(cnpj);
        }

        public IEnumerable<HealthInsuranceOperator> GetByName(string name)
        {
            return _repository.GetByName(name);
        }

        public HealthInsuranceOperator Add(HealthInsuranceOperator healthOperator)
        {
            return _repository.Add(healthOperator);
        }

        public void Update(HealthInsuranceOperator healthOperator)
        {
            _repository.Update(healthOperator);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}