using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using HealthPlan.Quote.Services.HealthPlan.Interface;

namespace HealthPlan.Quote.Services.HealthPlan.Implementation
{
    public class PriceTableService : IPriceTableService
    {
        private readonly IPriceTableRepository _repository;

        public PriceTableService(IPriceTableRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<PriceTable> GetAll()
        {
            return _repository.GetAll();
        }

        public PriceTable? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<PriceTable> GetByHealthPlanId(int healthPlanId)
        {
            return _repository.GetByHealthPlanId(healthPlanId);
        }

        public IEnumerable<PriceTable> GetByAgeRangeId(int ageRangeId)
        {
            return _repository.GetByAgeRangeId(ageRangeId);
        }

        public IEnumerable<PriceTable> GetActivePrices(int healthPlanId, DateTime date)
        {
            return _repository.GetActivePrices(healthPlanId, date);
        }

        public PriceTable? GetCurrentPrice(int healthPlanId, int ageRangeId)
        {
            return _repository.GetCurrentPrice(healthPlanId, ageRangeId);
        }

        public PriceTable Add(PriceTable priceTable)
        {
            return _repository.Add(priceTable);
        }

        public void Update(PriceTable priceTable)
        {
            _repository.Update(priceTable);
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}