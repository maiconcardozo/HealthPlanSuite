using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class PlanCoverageService : IPlanCoverageService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public PlanCoverageService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanCoverage> GetAll()
        {
            return _unitOfWork.PlanCoverageRepository.GetAll();
        }

        public PlanCoverage? GetById(int id)
        {
            return _unitOfWork.PlanCoverageRepository.GetById(id);
        }

        public IEnumerable<PlanCoverage> GetByHealthPlanId(int healthPlanId)
        {
            return _unitOfWork.PlanCoverageRepository.GetByHealthPlanId(healthPlanId);
        }

        public IEnumerable<PlanCoverage> GetByHealthEstablishmentId(int healthEstablishmentId)
        {
            return _unitOfWork.PlanCoverageRepository.GetByHealthEstablishmentId(healthEstablishmentId);
        }

        public bool ExistsCoverage(int healthPlanId, int healthEstablishmentId)
        {
            return _unitOfWork.PlanCoverageRepository.ExistsCoverage(healthPlanId, healthEstablishmentId);
        }

        public PlanCoverage Add(PlanCoverage planCoverage)
        {
            PlanCoverage result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.PlanCoverageRepository.Add(planCoverage);
            });
            return result;
        }

        public void Update(PlanCoverage planCoverage)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PlanCoverageRepository.Update(planCoverage);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PlanCoverageRepository.Delete(id);
            });
        }
    }
}