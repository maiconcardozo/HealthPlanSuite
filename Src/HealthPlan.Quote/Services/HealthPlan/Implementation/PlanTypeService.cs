using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class PlanTypeService : IPlanTypeService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public PlanTypeService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanType> GetAll()
        {
            return _unitOfWork.PlanTypeRepository.GetAll();
        }

        public PlanType? GetById(int id)
        {
            return _unitOfWork.PlanTypeRepository.GetById(id);
        }

        public IEnumerable<PlanType> GetByDescription(string description)
        {
            return _unitOfWork.PlanTypeRepository.GetByDescription(description);
        }

        public PlanType Add(PlanType planType)
        {
            PlanType result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.PlanTypeRepository.Add(planType);
            });
            return result;
        }

        public void Update(PlanType planType)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PlanTypeRepository.Update(planType);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PlanTypeRepository.Delete(id);
            });
        }
    }
}