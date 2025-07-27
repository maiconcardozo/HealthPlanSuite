using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IPriceTableService
    {
        IEnumerable<PriceTable> GetAll();
        PriceTable? GetById(int id);
        IEnumerable<PriceTable> GetByHealthPlanId(int healthPlanId);
        IEnumerable<PriceTable> GetByAgeRangeId(int ageRangeId);
        IEnumerable<PriceTable> GetActivePrices(int healthPlanId, DateTime date);
        PriceTable? GetCurrentPrice(int healthPlanId, int ageRangeId);
        PriceTable Add(PriceTable priceTable);
        void Update(PriceTable priceTable);
        void Delete(int id);
    }
}