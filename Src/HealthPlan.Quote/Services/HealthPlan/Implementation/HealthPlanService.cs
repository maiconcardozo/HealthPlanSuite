using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class HealthPlanService : IHealthPlanService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public HealthPlanService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetAll()
        {
            return _unitOfWork.HealthPlanRepository.GetAll();
        }

        public Domain.HealthPlan.Implementation.HealthPlan? GetById(int id)
        {
            return _unitOfWork.HealthPlanRepository.GetById(id);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByOperatorId(int operatorId)
        {
            return _unitOfWork.HealthPlanRepository.GetByOperatorId(operatorId);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByPlanTypeId(int planTypeId)
        {
            return _unitOfWork.HealthPlanRepository.GetByPlanTypeId(planTypeId);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetByName(string name)
        {
            return _unitOfWork.HealthPlanRepository.GetByName(name);
        }

        public IEnumerable<Domain.HealthPlan.Implementation.HealthPlan> GetWithCoverage()
        {
            return _unitOfWork.HealthPlanRepository.GetWithCoverage();
        }

        public Domain.HealthPlan.Implementation.HealthPlan Add(Domain.HealthPlan.Implementation.HealthPlan healthPlan)
        {
            Domain.HealthPlan.Implementation.HealthPlan result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.HealthPlanRepository.Add(healthPlan);
            });
            return result;
        }

        public void Update(Domain.HealthPlan.Implementation.HealthPlan healthPlan)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.HealthPlanRepository.Update(healthPlan);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.HealthPlanRepository.Delete(id);
            });
        }
    }
}