using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class AgeRangeService : IAgeRangeService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public AgeRangeService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AgeRange> GetAll()
        {
            return _unitOfWork.AgeRangeRepository.GetAll();
        }

        public AgeRange? GetById(int id)
        {
            return _unitOfWork.AgeRangeRepository.GetById(id);
        }

        public AgeRange? GetByAge(int age)
        {
            return _unitOfWork.AgeRangeRepository.GetByAge(age);
        }

        public IEnumerable<AgeRange> GetActiveRanges()
        {
            return _unitOfWork.AgeRangeRepository.GetActiveRanges();
        }

        public AgeRange Add(AgeRange ageRange)
        {
            AgeRange result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.AgeRangeRepository.Add(ageRange);
            });
            return result;
        }

        public void Update(AgeRange ageRange)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AgeRangeRepository.Update(ageRange);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.AgeRangeRepository.Delete(id);
            });
        }
    }
}