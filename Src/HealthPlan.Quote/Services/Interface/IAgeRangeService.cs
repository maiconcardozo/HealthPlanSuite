using HealthPlan.Quote.Domain.Implementation;

namespace HealthPlan.Quote.Services.Interface
{
    /// <summary>
    /// Service interface for AgeRange business operations.
    /// Provides business logic layer for AgeRange management.
    /// </summary>
    public interface IAgeRangeService
    {
        IEnumerable<AgeRange> GetAllActiveAgeRanges();
        AgeRange? GetById(int id);
        void AddAgeRange(AgeRange ageRange);
        void UpdateAgeRange(AgeRange ageRange);
        void DeleteAgeRange(int id);
    }
}