using HealthPlan.Quote.Domain.HealthPlan.Implementation;

namespace HealthPlan.Quote.Services.HealthPlan.Interface
{
    public interface IAgeRangeService
    {
        IEnumerable<AgeRange> GetAll();
        AgeRange? GetById(int id);
        AgeRange? GetByAge(int age);
        IEnumerable<AgeRange> GetByAgeRange(int minAge, int maxAge);
        AgeRange Add(AgeRange ageRange);
        void Update(AgeRange ageRange);
        void Delete(int id);
    }
}