using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class HealthInsuranceOperatorService : IHealthInsuranceOperatorService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public HealthInsuranceOperatorService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<HealthInsuranceOperator> GetAll()
        {
            return _unitOfWork.HealthInsuranceOperatorRepository.GetAll();
        }

        public HealthInsuranceOperator? GetById(int id)
        {
            return _unitOfWork.HealthInsuranceOperatorRepository.GetById(id);
        }

        public HealthInsuranceOperator? GetByCNPJ(string cnpj)
        {
            return _unitOfWork.HealthInsuranceOperatorRepository.GetByCNPJ(cnpj);
        }

        public IEnumerable<HealthInsuranceOperator> GetByName(string name)
        {
            return _unitOfWork.HealthInsuranceOperatorRepository.GetByName(name);
        }

        public HealthInsuranceOperator Add(HealthInsuranceOperator healthOperator)
        {
            HealthInsuranceOperator result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.HealthInsuranceOperatorRepository.Add(healthOperator);
            });
            return result;
        }

        public void Update(HealthInsuranceOperator healthOperator)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.HealthInsuranceOperatorRepository.Update(healthOperator);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.HealthInsuranceOperatorRepository.Delete(id);
            });
        }
    }
}