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

        public HealthInsuranceOperator Add(HealthInsuranceOperator @operator)
        {
            return _repository.Add(@operator);
        }

        public void Update(HealthInsuranceOperator @operator)
        {
            _repository.Update(@operator);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}