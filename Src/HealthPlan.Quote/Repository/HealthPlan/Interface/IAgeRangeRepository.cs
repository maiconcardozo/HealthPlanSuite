using Foundation.Base.Repository.Interface;
using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Repository.HealthPlan.Interface
{
    public interface IAgeRangeRepository : IEntityRepository<AgeRange>
    {
        AgeRange? GetByAge(int age);
        IEnumerable<AgeRange> GetByAgeRange(int minAge, int maxAge);
    }
}