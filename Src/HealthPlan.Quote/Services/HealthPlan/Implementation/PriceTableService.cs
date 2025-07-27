using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Services.HealthPlan.Interface;
using HealthPlan.Quote.UnitOfWork.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class PriceTableService : IPriceTableService
    {
        private readonly IHealthPlanUnitOfWork _unitOfWork;

        public PriceTableService(IHealthPlanUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PriceTable> GetAll()
        {
            return _unitOfWork.PriceTableRepository.GetAll();
        }

        public PriceTable? GetById(int id)
        {
            return _unitOfWork.PriceTableRepository.GetById(id);
        }

        public IEnumerable<PriceTable> GetByHealthPlanId(int healthPlanId)
        {
            return _unitOfWork.PriceTableRepository.GetByHealthPlanId(healthPlanId);
        }

        public IEnumerable<PriceTable> GetByAgeRangeId(int ageRangeId)
        {
            return _unitOfWork.PriceTableRepository.GetByAgeRangeId(ageRangeId);
        }

        public IEnumerable<PriceTable> GetActivePrices(int healthPlanId, DateTime date)
        {
            return _unitOfWork.PriceTableRepository.GetActivePrices(healthPlanId, date);
        }

        public PriceTable? GetCurrentPrice(int healthPlanId, int ageRangeId)
        {
            return _unitOfWork.PriceTableRepository.GetCurrentPrice(healthPlanId, ageRangeId);
        }

        public PriceTable Add(PriceTable priceTable)
        {
            PriceTable result = null;
            _unitOfWork.ExecuteInTransaction(() =>
            {
                result = _unitOfWork.PriceTableRepository.Add(priceTable);
            });
            return result;
        }

        public void Update(PriceTable priceTable)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PriceTableRepository.Update(priceTable);
            });
        }

        public void Delete(int id)
        {
            _unitOfWork.ExecuteInTransaction(() =>
            {
                _unitOfWork.PriceTableRepository.Delete(id);
            });
        }
    }
}