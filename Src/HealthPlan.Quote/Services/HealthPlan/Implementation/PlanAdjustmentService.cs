using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class PlanAdjustmentService : IPlanAdjustmentService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public PlanAdjustmentService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanAdjustment> GetAll()
        {
            return _unitOfWork.PlanAdjustmentRepository.GetAll();
        }

        public PlanAdjustment? GetById(int id)
        {
            return _unitOfWork.PlanAdjustmentRepository.GetById(id);
        }

        public IEnumerable<PlanAdjustment> GetByHealthPlanId(int healthPlanId)
        {
            return _unitOfWork.PlanAdjustmentRepository.GetByHealthPlanId(healthPlanId);
        }

        public IEnumerable<PlanAdjustment> GetByEffectiveDate(DateTime effectiveDate)
        {
            return _unitOfWork.PlanAdjustmentRepository.GetByEffectiveDate(effectiveDate);
        }

        public PlanAdjustment? GetCurrentAdjustment(int healthPlanId)
        {
            return _unitOfWork.PlanAdjustmentRepository.GetCurrentAdjustment(healthPlanId);
        }

        public PlanAdjustment Add(PlanAdjustment planAdjustment)
        {
            PlanAdjustment result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.PlanAdjustmentRepository.Add(planAdjustment);
            });
            return result;
        }

        public void Update(PlanAdjustment planAdjustment)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PlanAdjustmentRepository.Update(planAdjustment);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PlanAdjustmentRepository.Delete(id);
            });
        }
    }
}