using Foundation.Base.Repository.Implementation;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;
using HealthPlan.Quote.Repository.HealthPlan.Interface;
using Microsoft.EntityFrameworkCore;

namespace HealthPlan.Quote.Repository.HealthPlan.Implementation
{
    public class AgeRangeRepository : EntityRepository<AgeRange>, IAgeRangeRepository
    {
        public AgeRangeRepository(DbContext context) : base(context)
        {
        }

        public AgeRange? GetByAge(int age)
        {
            return Context.Set<AgeRange>()
                .FirstOrDefault(x => x.MinAge <= age && x.MaxAge >= age);
        }

        public IEnumerable<AgeRange> GetByAgeRange(int minAge, int maxAge)
        {
            return Context.Set<AgeRange>()
                .Where(x => x.MinAge >= minAge && x.MaxAge <= maxAge)
                .ToList();
        }
    }
}