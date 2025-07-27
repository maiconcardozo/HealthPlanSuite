using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class PriceTableRepository : EntityRepository<PriceTable>, IPriceTableRepository
    {
        public PriceTableRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<PriceTable> GetByHealthPlanId(int healthPlanId)
        {
            return Context.Set<PriceTable>()
                .Include(x => x.HealthPlan)
                .Include(x => x.AgeRange)
                .Where(x => x.HealthPlanId == healthPlanId)
                .ToList();
        }

        public IEnumerable<PriceTable> GetByAgeRangeId(int ageRangeId)
        {
            return Context.Set<PriceTable>()
                .Include(x => x.HealthPlan)
                .Include(x => x.AgeRange)
                .Where(x => x.AgeRangeId == ageRangeId)
                .ToList();
        }

        public IEnumerable<PriceTable> GetActivePrices(int healthPlanId, DateTime date)
        {
            return Context.Set<PriceTable>()
                .Include(x => x.HealthPlan)
                .Include(x => x.AgeRange)
                .Where(x => x.HealthPlanId == healthPlanId &&
                           x.StartDate <= date &&
                           (x.EndDate == null || x.EndDate >= date))
                .ToList();
        }

        public PriceTable? GetCurrentPrice(int healthPlanId, int ageRangeId)
        {
            var currentDate = DateTime.Now;
            return Context.Set<PriceTable>()
                .Include(x => x.HealthPlan)
                .Include(x => x.AgeRange)
                .FirstOrDefault(x => x.HealthPlanId == healthPlanId &&
                                    x.AgeRangeId == ageRangeId &&
                                    x.StartDate <= currentDate &&
                                    (x.EndDate == null || x.EndDate >= currentDate));
        }
    }
}