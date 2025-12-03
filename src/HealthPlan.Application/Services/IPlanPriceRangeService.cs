using HealthPlan.Domain.Entities;

namespace HealthPlan.Application.Services
{
    /// <summary>
    /// Service interface for PlanPriceRange business operations.
    /// Provides business logic layer for PlanPriceRange management.
    /// </summary>
    public interface IPlanPriceRangeService
    {
        IEnumerable<PlanPriceRange> GetAllActivePlanPriceRanges();
        PlanPriceRange? GetById(int id);
        void AddPlanPriceRange(PlanPriceRange planPriceRange);
        void UpdatePlanPriceRange(PlanPriceRange planPriceRange);
        void DeletePlanPriceRange(int id);
    }
}