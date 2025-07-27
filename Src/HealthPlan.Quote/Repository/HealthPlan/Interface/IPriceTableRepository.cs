using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IPriceTableRepository : IEntityRepository<PriceTable>
    {
        IEnumerable<PriceTable> GetByHealthPlanId(int healthPlanId);
        IEnumerable<PriceTable> GetByAgeRangeId(int ageRangeId);
        IEnumerable<PriceTable> GetActivePrices(int healthPlanId, DateTime date);
        PriceTable? GetCurrentPrice(int healthPlanId, int ageRangeId);
    }
}