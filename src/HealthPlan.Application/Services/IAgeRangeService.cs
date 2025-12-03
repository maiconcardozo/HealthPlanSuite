using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
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